using _Scripts.Environment;
using _Scripts.Utility.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace _Scripts.Units.Utility
{
    public class DeathMenu : TimeListener
    {
        [SerializeField] private GameObject deathMenu;
        [SerializeField] private TextMeshProUGUI text;
        private bool _active;
        private InputAction _controls;

        private void Start()
        {
            _controls = new PlayerControl().UI.Submit;
            _controls.Enable();
        }

        private void Update()
        {
            if (!deathMenu.activeSelf) return;
            OnDeath();
            _active = true;
        }

        public void OnDeath()
        {
            if (_active) return;
            Time.timeScale = 0f;
            _controls.performed += SubmitOnPerformed;
        }

        private void SubmitOnPerformed(InputAction.CallbackContext context)
        {
            if (!deathMenu.activeSelf) return;

            SerializationManager.Instance.SaveGame();
            SceneManager.LoadSceneAsync(0); // Main menu
        }

        protected override void OnDateTimeChanged(int day, int hour, int min)
        {
            text.text = $"Days survived: {day}";
        }
    }
}