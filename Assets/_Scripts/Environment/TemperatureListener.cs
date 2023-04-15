using UnityEngine;

namespace _Scripts.Environment
{
    public abstract class TemperatureListener : MonoBehaviour
    {
        private void OnEnable()
        {
            TemperatureController.OnTemperatureChange += OnTemperatureChange;
        }

        private void OnDisable()
        {
            TemperatureController.OnTemperatureChange -= OnTemperatureChange;
        }

        protected abstract void OnTemperatureChange(float min, float current, float max);
    }
}