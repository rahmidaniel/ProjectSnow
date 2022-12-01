using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace _Scripts.Environment
{
    public class TimeController : MonoBehaviour
    {
        [SerializeField] private int day, hour, minute;

        [Header("Tick settings")]
        [SerializeField] private float tickDistance;
        [SerializeField, Range(1,60)] private int tickMinuteIncrease;
        private float _sinceLastTick;
        
        public static UnityAction<int, int, int> OnDateTimeChanged;

        private void Start()
        {
            OnDateTimeChanged?.Invoke(day, hour, minute);
        }

        private void Update()
        {
            _sinceLastTick += Time.deltaTime;

            if (_sinceLastTick >= tickDistance)
            {
                _sinceLastTick = 0;
                Tick();
            }
        }

        private void Tick()
        {
            minute += tickMinuteIncrease;
            if (minute >= 60)
            {
                minute = 0;
                hour++;
            }

            if (hour >= 24)
            {
                hour = 0;
                day++;
            }
            OnDateTimeChanged?.Invoke(day, hour, minute);
        }
    }
}