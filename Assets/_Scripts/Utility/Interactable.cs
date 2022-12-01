using _Scripts.Environment;
using TMPro;
using UnityEngine;

namespace _Scripts.Utility
{
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField] protected GameObject actionPanel;
        private InputController _controller;
        private TextMeshProUGUI _text;
        private bool _enter;
        
        protected abstract string UpdateMessage();
        protected abstract void Interact();
        
        /// <summary>
        /// Define as empty function if there is no need for an entry event
        /// </summary>
        protected abstract void OnPlayerEnter();
        /// <summary>
        /// Define as empty function if there is no need for an entry event
        /// </summary>
        protected abstract void OnPlayerExit();

        public void Start() 
        {
            _controller = GameManager.Instance.Controller;
            _text = actionPanel.GetComponentInChildren<TextMeshProUGUI>();
        }

        public void Update()
        {
            if (_enter)
            {
                if(_controller.RetrieveInteractInput()) Interact();
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _enter = true;
                actionPanel.SetActive(true);
                _text.text = UpdateMessage();
                OnPlayerEnter();
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                _enter = false;
                actionPanel.SetActive(false);
                _text.text = "";
                OnPlayerExit();
            }
        }

    }
}