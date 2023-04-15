using _Scripts.Environment;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class TemperatureDisplay : TemperatureListener
    {
        [SerializeField] private Slider bar;
        [SerializeField] private Gradient gradient;
        [SerializeField] private Image fill;
        [SerializeField] private float changeSpeed = 100f;
        private float _currentValue;
        private float _currentVelocity;
        private float _targetValue;
        private TextMeshProUGUI _text;


        private void Start()
        {
            bar.interactable = false;
            _text = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Update()
        {
            bar.value = Mathf.SmoothDamp(_currentValue, _targetValue, ref _currentVelocity,
                changeSpeed * Time.deltaTime);
        }

        protected override void OnTemperatureChange(float min, float current, float max)
        {
            SetRange(min, max);
            SetBarValue(current);
            SetBarText((int) current + " °C | " + (int) max);
        }

        private void SetBarValue(float value)
        {
            _currentValue = bar.value;
            _targetValue = value;
            fill.color = gradient.Evaluate(bar.normalizedValue);
        }

        private void SetBarText(string text)
        {
            if (_text != null) _text.text = text;
        }

        private void SetRange(float min, float max)
        {
            bar.minValue = min;
            bar.maxValue = max;
        }
    }
}