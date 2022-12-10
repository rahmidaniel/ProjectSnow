using System;
using _Scripts.Utility;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Environment
{
    public class TemperatureController : MonoBehaviour
    {
        [SerializeField] private float min = -10;
        [SerializeField] private float current = 30;
        [SerializeField] private float max = 30;
        [SerializeField] private float cappedMax;
        [SerializeField] private float fallRate = 1f;
        [SerializeField] private float gainRate = 3.5f;
        
        [SerializeField] private float ttl = 120;
        
        public static UnityAction<float, float, float> OnTemperatureChange;


        private float _currentVelocity;
        [SerializeField] private float delta;

        private void Start()
        {
            OnTemperatureChange?.Invoke(min, current, max);
        }

        private void Update()
        {
            cappedMax = max - (1 - Player.Instance.HouseInfo.Integrity) * (max - min);
            var diff =  Time.deltaTime * ((max - min) / ttl);

            switch (Player.Instance.HouseInfo.State)
            {
                case HouseState.Inside:
                    current += diff * gainRate;
                    if (current > cappedMax) current = cappedMax;
                    break;
                case HouseState.InsideOpen:
                    current += diff * (gainRate / 2);
                    if (current > cappedMax) current = cappedMax;
                    break;
                case HouseState.Outside or HouseState.OutsideOpen:
                    current -= diff * fallRate;
                    if (current < min) current = min;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            OnTemperatureChange?.Invoke(min, current, max);
        }
    }
}