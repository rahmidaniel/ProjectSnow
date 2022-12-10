using System;
using _Scripts.Environment;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace _Scripts.Units.Utility
{
    public class UIBar : MonoBehaviour
    {
        [SerializeField] private Slider bar;
        [SerializeField] private Gradient gradient;
        [SerializeField] private Image fill;
        [SerializeField] private float changeSpeed = 100f;
        private float _currentVelocity;
        private float _currentValue;
        private float _targetValue;
        private TextMeshProUGUI _text;
        

        private void Start()
        {
            bar.interactable = false;
            _text = GetComponentInChildren<TextMeshProUGUI>();
            TemperatureController.OnTemperatureChange += OnTemperatureChange;
        }
        
        private void OnDestroy()
        {
            TemperatureController.OnTemperatureChange -= OnTemperatureChange;
        }

        private void OnTemperatureChange(float min, float current, float max)
        { 
            SetRange(min, max);
            SetBarValue(current);
            SetBarText((int)current + " °C | " + (int)max);
        }

        private void Update()
        {
            bar.value = Mathf.SmoothDamp(_currentValue, _targetValue, ref _currentVelocity, changeSpeed * Time.deltaTime);
        }

        private void SetBarValue(float value)
        {
            //bar.value = Mathf.SmoothDamp(bar.value, value, ref _currentVelocity, changeSpeed * Time.deltaTime);
            _currentValue = bar.value;
            _targetValue = value;
            //bar.value = value;
            fill.color = gradient.Evaluate(bar.normalizedValue);
        }

        private void SetBarText(string text)
        {
            _text.text = text;
        }

        private void SetRange(float min, float max)
        {
            bar.minValue = min;
            bar.maxValue = max;
        }
    }
}