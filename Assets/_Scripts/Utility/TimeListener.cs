using System;
using _Scripts.Environment;
using TMPro;
using UnityEngine;

namespace _Scripts.Utility
{
    public class TimeListener : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        private void Start()
        {
            TimeController.OnDateTimeChanged += OnDateTimeChanged;
        }
        private void OnDestroy()
        {
            TimeController.OnDateTimeChanged -= OnDateTimeChanged;
        }

        private void OnDateTimeChanged(int day, int hour, int minute)
        {
            text.text = $"Day: {day}\n {hour:00}:{minute:00}";
        }
    }
}
