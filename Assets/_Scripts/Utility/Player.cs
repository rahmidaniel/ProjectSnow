using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Environment;
using _Scripts.UI;
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
        private bool _dead;
        
        private void Awake()
        {
            if (Instance != null)
            {
                //Debug.Log("Existing 'PLAYER' instance found, newest destroyed.");
                Destroy(gameObject);
                return;
            }
            
            _movementController = GetComponent<Move>();
            _playerInput = GetComponent<PlayerInput>();
            
            Instance = this;
            DontDestroyOnLoad(gameObject);

            HouseInfo = new HouseInfo {Integrity = 1f, State = HouseState.Outside};
        }

        private void Start()
        {
            TemperatureController.OnTemperatureChange += OnTemperatureChange;
        }

        private void OnTemperatureChange(float min, float current, float max)
        {
            // TODO: Will trigger backwards
            if(Math.Abs(current - (max - min) * 0.2f) < 0.01) PlayerAnimator.Instance.FrozenBreathSound();
            
            if (Mathf.Abs(min - current) < 0.1f && !_dead)
            {
                _dead = true;
                PlayerAnimator.Instance.ChangeAnimation(PlayerAnimationState.Death);
                //_movementController.blocked = true; // TODO messes up animation somehow
            }

        }

        private void Update()
        {
            if(!_dead) return;
            if(PlayerAnimator.Instance.IsAnimationFinished(PlayerAnimationState.Death)) 
                UIManager.UIStateChanged.Invoke(UIState.DeathMenu);
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
            data.dead = _dead;
            data.playerPosition = transform.position;
            data.logCount = logCount;
            data.sceneIndex = SceneManager.GetActiveScene().buildIndex;
        }

        public void LoadData(GameData data)
        {
            transform.position = data.playerPosition;
            logCount = data.logCount;
        }

        private void OnDestroy()
        {
            TemperatureController.OnTemperatureChange -= OnTemperatureChange;
        }
    }
}