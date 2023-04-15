using _Scripts.Environment;
using TMPro;
using UnityEngine;

namespace _Scripts.UI
{
    public class TimeDisplay : Environment.TimeListener
    {
        [SerializeField] private TextMeshProUGUI text;

        protected override void OnDateTimeChanged(int day, int hour, int minute)
        {
            text.text = $"Day: {day}\n {hour:00}:{minute:00}";
        }
    }
}