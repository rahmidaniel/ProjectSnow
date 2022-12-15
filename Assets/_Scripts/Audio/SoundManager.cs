using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace _Scripts.Utility
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }

        [Header("Settings")] [SerializeField, Range(0, 1)] public float masterVolume;
        private Bus _masterBus;

        private List<EventInstance> _eventInstances;
        private List<StudioEventEmitter> _eventEmitters;

        private EventInstance _musicEventInstance;

        private EventInstance _ambienceEventInstance;

        private void Awake()
        {
            if (Instance != null)
            {
                //Debug.Log("Existing 'SOUND MANAGER' instance found, newest destroyed.");
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(gameObject);

            _eventInstances = new List<EventInstance>();
            _eventEmitters = new List<StudioEventEmitter>();
            _masterBus = RuntimeManager.GetBus("bus:/");

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            StopMusic();
            StopAmbience();
            switch (scene.buildIndex)
            {
                case 0:
                    PlayMusic(FMODEvents.Instance.Menu);
                    break;
                case 1:
                    PlayAmbience();
                    SetAmbienceArea(AmbienceArea.Mountain);
                    break;
                case 2:
                    PlayAmbience();
                    SetAmbienceArea(AmbienceArea.Forest);
                    break;
            }
        }

        private void Update()
        {
            _masterBus.setVolume(masterVolume);
        }

        public static void PlayOneShot(EventReference sound, Vector3 position)
        {
            RuntimeManager.PlayOneShot(sound, position);
        }

        public EventInstance CreateEventInstance(EventReference reference)
        {
            var eventInstance = RuntimeManager.CreateInstance(reference);
            _eventInstances.Add(eventInstance);
            return eventInstance;
        }

        #region Emitters

        public StudioEventEmitter CreateStudioEventEmitter(EventReference reference, GameObject attachedObject)
        {
            var eventEmitter = attachedObject.GetComponent<StudioEventEmitter>();
            eventEmitter.EventReference = reference;

            _eventEmitters.Add(eventEmitter);
            return eventEmitter;
        }

        #endregion

        #region Music
        public void SetMusicEvent(EventReference reference)
        {
            _musicEventInstance = CreateEventInstance(reference);
        }
        public void PlayMusic(EventReference reference)
        {
            // If there is another music playing
            SetMusicEvent(reference);

            _musicEventInstance.start();
            _musicEventInstance.release();
        }
        public void StopMusic()
        {
            _musicEventInstance.stop(STOP_MODE.ALLOWFADEOUT);
        }

        #endregion
        
        #region Ambience
        private void SetAmbienceInstance(EventReference reference)
        {
            _ambienceEventInstance = CreateEventInstance(reference);
        }
        
        public void PlayAmbience()
        {
            // If it hasn't been initialized
            if (!_ambienceEventInstance.isValid())
            {
                SetAmbienceInstance(FMODEvents.Instance.Ambience);
            }

            _ambienceEventInstance.start();
            _ambienceEventInstance.release();
        }

        public void StopAmbience()
        {
            _ambienceEventInstance.stop(STOP_MODE.ALLOWFADEOUT);
        }

        public void SetAmbienceParameter(string paramName, float paramValue)
        {
            _ambienceEventInstance.setParameterByName(paramName, paramValue);
        }
        // Enums are easier to use ( but not very pretty here )
        public void SetAmbienceArea(AmbienceArea area) => SetAmbienceParameter("area", (float) area);
        public void SetForestAmbiance(ForestAmbiance mode) => SetAmbienceParameter("forest", (float) mode);
        public void SetMountainAmbiance(MountainAmbiance mode) => SetAmbienceParameter("mountain", (float) mode);
        
        #endregion
        
        private void CleanUp()
        {
            if (_eventInstances == null) return;
            foreach (var eventInstance in _eventInstances)
            {
                eventInstance.stop(STOP_MODE.ALLOWFADEOUT);
                eventInstance.release();
            }
            if (_eventEmitters == null) return;
            foreach (var eventEmitter in _eventEmitters)
            {
                eventEmitter.Stop();
            }
        }

        private void OnDestroy()
        {
            CleanUp();
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}