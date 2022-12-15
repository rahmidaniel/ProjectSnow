using System;
using _Scripts.Environment;
using _Scripts.UI;
using _Scripts.Utility;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace _Scripts.Units.Utility
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool IsPaused;
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private Button continueButton;
        private PlayerControl _inputActions;
        private InputAction _menu;

        private void Awake()
        {
            _inputActions = new PlayerControl();
        }

        private void OnEnable()
        {
            _menu = _inputActions.UI.Escape;
            _menu.Enable();

            _menu.performed += Escape;
        }

        private void OnDisable()
        {
            _menu.Disable();
        }

        public void Escape(InputAction.CallbackContext context)
        {
            if(!IsPaused) Pause();
            else Resume();
        }

        public void Resume()
        {
            UIManager.UIStateChanged.Invoke(UIState.HUD);
            Player.Instance.DisableMovement(false);
            Time.timeScale = 1f;
            IsPaused = false;
        }

        public void Pause()
        {
            continueButton.Select();
            UIManager.UIStateChanged.Invoke(UIState.PauseMenu);
            Player.Instance.DisableMovement(true);
            Time.timeScale = 0f;
            IsPaused = true;
        }
        
        public void Exit()
        {
            Application.Quit();
        }
    }
}