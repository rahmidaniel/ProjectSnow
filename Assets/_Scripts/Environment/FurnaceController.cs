using System;
using _Scripts.Utility;
using _Scripts.Utility.Serialization;
using FMODUnity;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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

        private Collider2D _collider2D;
        private SpriteRenderer _body;

        private StudioEventEmitter _emitter;

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
        private FireAmbiance _fireAmbience;

        protected override string UpdateMessage()
        {
            return "Press 'F' to feed the flames. (" + (int) fuel + "/" + (int) maxFuel + ")";
        }

        private void Start()
        {
            _body = GetComponent<SpriteRenderer>();
            _emitter = SoundManager.Instance.CreateStudioEventEmitter(FMODEvents.Instance.Fire, gameObject);
            _emitter.Play();
        }

        private void OnDestroy()
        {
            _emitter.Stop();
        }

        private void Update()
        {
            if (fuel > 0)
            {
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
        
            Player.Instance.HouseInfo.Integrity = fuel / maxFuel;
            UpdateSound();
        }

        private void UpdateSound()
        {
            // 33%
            var value = Player.Instance.HouseInfo.Integrity;
            if (FireAmbiance.High != FireAmbience && value > 0.7f)
            {
                FireAmbience = FireAmbiance.High;
            }
            if (FireAmbiance.Medium != FireAmbience && value is < 0.7f and >= 0.3f)
            {
               FireAmbience = FireAmbiance.Medium;
            }
            if (FireAmbiance.Low != FireAmbience && value < 0.3f)
            {
                FireAmbience = FireAmbiance.Low;
            }
        }

        protected override void Interact()
        {
            if (Player.Instance.logCount == 0) return;

            Player.Instance.logCount--;
            fuel += gainLog;
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
    }
}