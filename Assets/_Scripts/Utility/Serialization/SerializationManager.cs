using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using _Scripts.Units.Utility;
using UnityEngine.SceneManagement;

namespace _Scripts.Utility.Serialization
{
    public class SerializationManager : MonoBehaviour
    {
        //[SerializeField] private string directory = "saves";
        [SerializeField] private string filename;
        [Header("Debug")]
        [SerializeField] private bool forceInit;
        private SaveFileHandler _saveFileHandler;
        public static SerializationManager Instance { get; private set; }
        public bool IsNewSave { get; private set; }
        private GameData _gameData;
        private List<IPersistentData> _persistentDataObjects;

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

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SaveGame();
            _persistentDataObjects = FindImplementations();
            LoadGame();
        }


        private static List<IPersistentData> FindImplementations()
        {
            var objects = FindObjectsOfType<MonoBehaviour>().OfType<IPersistentData>();
            return new List<IPersistentData>(objects);
        }

        public void NewGame()
        {
            _gameData = new GameData();
            IsNewSave = true;
        }
        public void LoadGame()
        {
            if(forceInit){
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
            foreach (var item in _persistentDataObjects)
            {
                item.LoadData(_gameData);
            }

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
            foreach (var item in _persistentDataObjects)
            {
                item.SaveData(ref _gameData);
            }

            _saveFileHandler.SaveData(_gameData);
        }

        public bool HasData()
        {
            return _gameData != null;
        }

        public int LastScene()
        {
            return _gameData.sceneIndex;
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }
    }
}