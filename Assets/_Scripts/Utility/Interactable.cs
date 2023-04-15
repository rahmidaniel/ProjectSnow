using _Scripts.Units;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace _Scripts.Utility
{
    public abstract class Interactable : MonoBehaviour
    {
        public static UnityAction<string> ShowMessage;
        public static UnityAction HideMessage;

        private bool _enter;

        private InputAction _interactAction;

        // Pretty print for UI elements
        protected string CurrentBinding =>
            _interactAction.GetBindingDisplayString(group: Player.Instance.GetCurrentControlScheme());

        private void OnEnable()
        {
            var c = new PlayerControl();
            _interactAction = c.Player.Fire;
            _interactAction.Enable();
            _interactAction.performed += OnFire;
        }

        private void OnDisable()
        {
            _interactAction.performed -= OnFire;
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _enter = true;
                // Send message to UI
                ShowMessage?.Invoke(UpdateMessage());
            }
        }

        protected void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _enter = false;
                // Send message to UI to hide the panel
                HideMessage?.Invoke();
            }
        }

        protected void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _enter = true;
                // Send message to UI
                ShowMessage?.Invoke(UpdateMessage());
            }
        }

        protected abstract string UpdateMessage();
        protected abstract void Interact();

        public void OnFire(InputAction.CallbackContext context)
        {
            if (_enter) Interact();
        }
    }
}