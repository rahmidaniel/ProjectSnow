using _Scripts.Utility.Serialization;
using UnityEngine;

namespace _Scripts.Units.Utility
{
    public class VideoController : MonoBehaviour, IPersistentData
    {
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

        public void SetQuality(int index)
        {
            QualitySettings.SetQualityLevel(index);
        }

        public void SetFullscreen(bool fullscreen)
        {
            Screen.fullScreen = fullscreen;
        }
    }
}