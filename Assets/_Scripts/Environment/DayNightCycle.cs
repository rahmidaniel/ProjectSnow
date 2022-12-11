using System;
using _Scripts.Utility;
using _Scripts.Utility.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace _Scripts.Environment
{
    public class DayNightCycle : MonoBehaviour
    {
        private static DayNightCycle Instance;
        
        [SerializeField] private float nightIntensity = 1;
        [SerializeField] private AnimationCurve lightCurve;
        [SerializeField] private Gradient lightGradient;

        private ColorAdjustments _colorAdjustments;
        private Volume _volume;
        
        private void Awake()
        {
            if (Instance != null)
            {
                //Debug.Log("Existing 'Managers' instance found, newest destroyed.");
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        private void Start()
        {
            _volume = GetComponent<Volume>();
            _volume.profile.TryGet(out _colorAdjustments);
            TimeController.OnDateTimeChanged += OnDateTimeChanged;
        }

        private void OnDateTimeChanged(int day, int hour, int minute)
        {
            var t = (hour * 60 + minute) / 1440f; // 24 * 60 total minutes
            var dayLight = lightCurve.Evaluate(t);
            _volume.weight = Mathf.Lerp(0, nightIntensity, dayLight);
            _colorAdjustments.colorFilter.value = lightGradient.Evaluate(dayLight);
        }
    }
}