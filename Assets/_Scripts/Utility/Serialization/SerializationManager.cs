using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Utility.Serialization
{
    public class SerializationManager : MonoBehaviour
    {
        //[SerializeField] private string directory = "saves";
        [SerializeField] private string filename;
        [Header("Debug")] [SerializeField] private bool forceInit;
        private GameData _gameData;
        private List<IPersistentData> _persistentDataObjects;
        private SaveFileHandler _saveFileHandler;
        public static SerializationManager Instance { get; private set; }
        public bool IsNewSave { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.Log("Existing 'SerializationManager' found, newest destroyed.");
                Destroy(gameObject);
                return;
            }

            Instance = this;
            _saveFileHandler = new SaveFileHandler(filename, Application.persistentDataPath);

            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if(_gameData != null) _gameData.sceneIndex = scene.buildIndex;
            _persistentDataObjects = FindImplementations();
            //if(!IsNewSave) LoadGame();
        }


        private static List<IPersistentData> FindImplementations()
        {
            var objects = FindObjectsOfType<MonoBehaviour>(true).OfType<IPersistentData>();
            return new List<IPersistentData>(objects);
        }

        public void NewGame()
        {
            _gameData = new GameData();
            IsNewSave = true;
        }

        public void LoadGame()
        {
            if (forceInit)
            {
                Debug.Log("Forcing new game.");
                NewGame(); // For debugging
                return;
            }

            // load from file
            _gameData = _saveFileHandler.LoadData();

            if (_gameData == null)
            {
                Debug.Log("No data found, aborting load.");
                return;
            }

            // pass data to other scripts
            foreach (var item in _persistentDataObjects) item.LoadData(_gameData);

            IsNewSave = false;
        }

        public void SaveGame()
        {
            if (_gameData == null)
            {
                Debug.Log("No data found, aborting save.");
                return;
            }

            // pass data to others scripts so they can update it
            foreach (var item in _persistentDataObjects) item.SaveData(ref _gameData);

            _saveFileHandler.SaveData(_gameData);
        }

        public bool HasData()
        {
            if (_gameData != null) return !_gameData.dead;
            return false;
        }

        public int LastScene()
        {
            return _gameData.sceneIndex;
        }
    }
}