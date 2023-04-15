using _Scripts.Utility;
using _Scripts.Utility.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI.Menus
{
    public class VolumeController : MonoBehaviour, IPersistentData
    {
        [SerializeField] private TextMeshProUGUI valueText;
        [SerializeField] private Slider slider;

        private void Start()
        {
            slider.onValueChanged.AddListener(OnValueChanged);
            if (SoundManager.Instance != null) slider.normalizedValue = SoundManager.Instance.masterVolume;
        }

        private void OnDestroy()
        {
            slider.onValueChanged.RemoveListener(OnValueChanged);
        }

        public void SaveData(ref GameData data)
        {
            data.masterVolume = SoundManager.Instance.masterVolume;
        }

        public void LoadData(GameData data)
        {
            SoundManager.Instance.masterVolume = data.masterVolume;
        }

        private void OnValueChanged(float volume)
        {
            valueText.text = (slider.normalizedValue * 100f).ToString("0") + "%";
            SoundManager.Instance.masterVolume = slider.normalizedValue;
        }
    }
}