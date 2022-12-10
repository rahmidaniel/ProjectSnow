using System;
using _Scripts.Environment;
using _Scripts.Utility.Serialization;
using UnityEngine;
using UnityEngine.Playables;

namespace _Scripts.Utility
{
    public class CutsceneController : MonoBehaviour
    {
        private PlayableDirector _director;
        // Cutscene should only play once
        private void OnEnable()
        {
            if (SerializationManager.Instance.IsNewSave)
            {
                Debug.Log("Playing cutscene.");
                gameObject.SetActive(true);
            }
            else gameObject.SetActive(false);
        }
    }
}