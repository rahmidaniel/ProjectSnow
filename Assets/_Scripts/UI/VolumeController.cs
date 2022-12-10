using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace _Scripts.UI.Menus
{
    public class VolumeController : MonoBehaviour
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private TextMeshProUGUI valueText;
        [SerializeField] private Slider slider;

        private void Start()
        {
            slider.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDestroy()
        {
            slider.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(float volume)
        {
            valueText.text = (slider.normalizedValue * 100f).ToString("0") + "%";
            audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20); // non linear
        }

    }
}