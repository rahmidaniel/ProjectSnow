using System;
using _Scripts.Environment;
using TMPro;
using UnityEngine;

namespace _Scripts.Utility
{
    public class TimeListener : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Start()
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
            TimeController.OnDateTimeChanged += OnDateTimeChanged;
        }

        private void OnDateTimeChanged(int day, int hour, int minute)
        {
            _text.text = $"Day: {day}\n {hour:00}:{minute:00}";
        }
    }
}
