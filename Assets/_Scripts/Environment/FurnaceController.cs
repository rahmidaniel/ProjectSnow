using _Scripts.Utility;
using _Scripts.Utility.Serialization;
using UnityEngine;

namespace _Scripts.Environment
{
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

        protected override string UpdateMessage()
        {
            return "Press 'F' to feed the flames. (" + (int) fuel + "/" + (int) maxFuel + ")";
        }

        private void Start()
        {
            _body = GetComponent<SpriteRenderer>();
        }

        private void LateUpdate()
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
            fuel = data.fuel;
            maxFuel = data.maxFuel;
        }
    }
}