using _Scripts.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace _Scripts.UI.Menus
{
    public class VolumeController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI valueText;
        [SerializeField] private Slider slider;

        private void Start()
        {
            slider.onValueChanged.AddListener(OnValueChanged);
            if(SoundManager.Instance != null) slider.normalizedValue = SoundManager.Instance.masterVolume;
        }

        private void OnDestroy()
        {
            slider.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(float volume)
        {
            valueText.text = (slider.normalizedValue * 100f).ToString("0") + "%";
            SoundManager.Instance.masterVolume = slider.normalizedValue;
        }

    }
}