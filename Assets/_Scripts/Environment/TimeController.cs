using _Scripts.Utility;
using _Scripts.Utility.Serialization;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Environment
{
    public class TimeController : MonoBehaviour, IPersistentData
    {
        public static UnityAction<int, int, int> OnDateTimeChanged;
        public static UnityAction<bool> OnDaytime;
        [SerializeField] private int day, hour, minute;

        [Header("Tick settings")] [SerializeField]
        private float tickDistance;

        [SerializeField] [Range(1, 60)] private int tickMinuteIncrease;

        [Header("Ambient Sound")] [SerializeField]
        private int dusk = 20;

        [SerializeField] private int dawn = 6;
        private float _sinceLastTick;

        private void Start()
        {
            OnDateTimeChanged?.Invoke(day, hour, minute);
            OnDaytime?.Invoke(hour < dusk && hour >= dawn);
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

        public void SaveData(ref GameData data)
        {
            data.time = new Vector3(day, hour, minute);
        }

        public void LoadData(GameData data)
        {
            if (data.temperature == Vector3.zero) return;
            day = (int) data.time[0];
            hour = (int) data.time[1];
            minute = (int) data.time[2];

            SoundManager.Instance.SetForestAmbiance(hour < dusk && hour > dawn
                ? ForestAmbiance.Day
                : ForestAmbiance.Night);
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

            if (hour == dusk)
            {
                OnDaytime?.Invoke(false);
                UpdateSound(ForestAmbiance.Night);
            }

            if (hour == dawn)
            {
                OnDaytime?.Invoke(true);
                UpdateSound(ForestAmbiance.Day);
            }
        }

        private void UpdateSound(ForestAmbiance mode)
        {
            SoundManager.Instance.SetForestAmbiance(mode);
        }
    }
}