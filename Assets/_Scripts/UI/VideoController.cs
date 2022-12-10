using TMPro;
using UnityEngine;
using UnityEngine.Audio;

namespace _Scripts.Units.Utility
{
    public class VideoController : MonoBehaviour
    {
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