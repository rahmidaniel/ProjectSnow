using System.Collections.Generic;
using System.Linq;
using _Scripts.Environment;
using _Scripts.Units.Capabilities;
using _Scripts.Utility.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace _Scripts.Utility
{
    public class Player : MonoBehaviour, IPersistentData
    {
        public HouseInfo HouseInfo;
        
        public static Player Instance { get; private set; }

        private Move _movementController;
        private PlayerInput _playerInput;
        
        public List<LogController> pickUpObjects; // TODO
        public int logCount;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.Log("Existing 'Managers' instance found, newest destroyed.");
                Destroy(gameObject);
                return;
            }
            
            _movementController = GetComponent<Move>();
            _playerInput = GetComponent<PlayerInput>();
            
            Instance = this;
            DontDestroyOnLoad(gameObject);

            HouseInfo = new HouseInfo();
            HouseInfo.Integrity = 1f;
            HouseInfo.State = HouseState.Outside;
        }
        
        public void TeleportToLevel()
        {
            var gate = GameObject.FindGameObjectsWithTag("Gate").First(gate => gate.scene == SceneManager.GetActiveScene());
            if (gate == null) transform.position = new Vector3();
            transform.position = gate.transform.position;
        }
        
        public void DisableMovement(bool value)
        {
            _movementController.blocked = value;
        }
        public bool CanMove()
        {
            return !_movementController.blocked;
        }

        public string GetCurrentControlScheme()
        {
            return _playerInput.currentControlScheme;
        }
        
        public void SaveData(ref GameData data)
        {
            data.playerPosition = _movementController.transform.position;
            data.logCount = logCount;
            data.sceneIndex = SceneManager.GetActiveScene().buildIndex;
        }

        public void LoadData(GameData data)
        {
            _movementController.transform.position = data.playerPosition;
            logCount = data.logCount;
        }
    }
}