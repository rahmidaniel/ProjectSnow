using _Scripts.Units;
using _Scripts.Utility;
using _Scripts.Utility.Serialization;
using FMODUnity;
using UnityEngine;

namespace _Scripts.Environment
{
    [RequireComponent(typeof(StudioEventEmitter))]
    public class FurnaceController : Interactable, IPersistentData
    {
        [SerializeField] private float fuel = 80f;
        [SerializeField] private float maxFuel = 100f;
        [SerializeField] private float fallRate = 1f;
        [SerializeField] private float openMulti = 3f;
        [SerializeField] private float gainLog = 25f;

        [SerializeField] private float ttl = 90f;
        [Header("Visuals")] [SerializeField] private GameObject effects;

        private Collider2D _collider2D;
        private StudioEventEmitter _emitter;

        private FireAmbiance _fireAmbience;
        private float _integrity;

        private bool _working;

        private FireAmbiance FireAmbience
        {
            get => _fireAmbience;
            set
            {
                // This isn't high cost because of the checks in UpdateSound
                _emitter.SetParameter("fire", (float) value);
                _fireAmbience = value;
            }
        }

        private void Start()
        {
            _emitter = SoundManager.Instance.CreateStudioEventEmitter(FMODEvents.Instance.Fire, gameObject);
            _working = true;
            _emitter.Play();
        }

        private void Update()
        {
            if (fuel > 0)
            {
                _working = true;
                var diff = Time.deltaTime * maxFuel / ttl;

                switch (Player.Instance.HouseInfo.State)
                {
                    case HouseState.InsideOpen or HouseState.OutsideOpen:
                        fuel -= diff * fallRate * openMulti;
                        break;
                    case HouseState.Inside or HouseState.Outside:
                        fuel -= diff * fallRate;
                        break;
                }
            }

            _integrity = fuel / maxFuel;
            switch (_working)
            {
                case true when fuel <= 0f:
                    EffectSwitch(false);
                    _working = false;
                    break;
                case false when fuel > 0f:
                    EffectSwitch(true);
                    _working = true;
                    break;
            }

            Player.Instance.HouseInfo.Integrity = _integrity;
            UpdateSound();
        }
        
        private void EffectSwitch(bool turnOn)
        {
            if (turnOn && !_emitter.IsPlaying()) _emitter.Play();
            else _emitter.Stop();
            effects.SetActive(turnOn);
        }

        private void OnDestroy()
        {
            _emitter.Stop();
        }

        public void SaveData(ref GameData data)
        {
            data.fuel = fuel;
            data.maxFuel = maxFuel;
        }

        public void LoadData(GameData data)
        {
            if (data.maxFuel == 0) return;
            fuel = data.fuel;
            maxFuel = data.maxFuel;
        }

        protected override string UpdateMessage()
        {
            return "Press 'F' to feed the flames. (" + (int) fuel + "/" + (int) maxFuel + ")";
        }

        private void UpdateSound()
        {
            // 33%
            if (FireAmbiance.High != FireAmbience && _integrity > 0.7f) FireAmbience = FireAmbiance.High;

            if (FireAmbiance.Medium != FireAmbience && _integrity is < 0.7f and >= 0.3f)
                FireAmbience = FireAmbiance.Medium;

            if (FireAmbiance.Low != FireAmbience && _integrity < 0.3f) FireAmbience = FireAmbiance.Low;
        }

        protected override void Interact()
        {
            if (Player.Instance.logCount == 0) return;

            Player.Instance.logCount--;
            fuel += gainLog;
        }
    }
}