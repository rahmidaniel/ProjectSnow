using System;
using _Scripts.Environment;
using _Scripts.UI;
using _Scripts.Units.Capabilities;
using _Scripts.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace _Scripts.Units.Utility
{
    public class DeathMenu : MonoBehaviour
    {
        [SerializeField] private GameObject deathMenu;
        [SerializeField] private TextMeshProUGUI text;
        private InputAction _controls;

        private void Start()
        {
            TimeController.OnDateTimeChanged += OnDateTimeChanged;

            _controls = new PlayerControl().UI.Submit;
            _controls.Enable();
            _controls.performed += SubmitOnPerformed;
        }

        private void Update()
        {
            if(deathMenu.activeSelf) Time.timeScale = 0f;
        }

        private void SubmitOnPerformed(InputAction.CallbackContext context)
        {
            if(deathMenu.activeSelf) SceneManager.LoadSceneAsync(2); // Main menu
        }

        private void OnDateTimeChanged(int day, int hour, int min)
        {
            text.text = $"Days survived: {day}";
        }

        // private void OnDeath()
        // {
        //     Player.Instance.DisableMovement(true);
        //     UIManager.UIStateChanged.Invoke(UIState.DeathMenu);
        //     Time.timeScale = 0f;
        // }
        
        private void OnDestroy()
        {
            TimeController.OnDateTimeChanged -= OnDateTimeChanged;
            if(_controls != null) _controls.performed -= SubmitOnPerformed;
        }
    }
}