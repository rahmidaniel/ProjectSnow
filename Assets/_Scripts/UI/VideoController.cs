using _Scripts.Utility.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

namespace _Scripts.Units.Utility
{
    public class VideoController : MonoBehaviour, IPersistentData
    {
        public void SetQuality(int index)
        {
            QualitySettings.SetQualityLevel(index);
        }

        public void SetFullscreen(bool fullscreen)
        {
            Screen.fullScreen = fullscreen;
        }

        public void SaveData(ref GameData data)
        {
            data.fullscreenOn = Screen.fullScreen;
            data.graphicsQuality = QualitySettings.GetQualityLevel();
        }

        public void LoadData(GameData data)
        {
            SetFullscreen(data.fullscreenOn);
            SetQuality(data.graphicsQuality);
        }
    }
}