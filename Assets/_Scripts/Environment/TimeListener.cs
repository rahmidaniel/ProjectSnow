using UnityEngine;

namespace _Scripts.Environment
{
    public abstract class TimeListener : MonoBehaviour
    {
        private void OnEnable()
        {
            TimeController.OnDateTimeChanged += OnDateTimeChanged;
        }

        private void OnDisable()
        {
            TimeController.OnDateTimeChanged -= OnDateTimeChanged;
        }
        protected abstract void OnDateTimeChanged(int day, int hour, int minute);
    }
}