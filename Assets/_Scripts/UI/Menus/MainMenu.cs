using System;
using _Scripts.Utility.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Scripts.Units.Utility
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button newGameButton; 
        [SerializeField] private Button continueButton;
        private bool _canContinue;

        private void Start()
        {
            continueButton.interactable = SerializationManager.Instance.HasData();
        }

        public void OnNewGameClick()
        {
            DisableButtons();
            SerializationManager.Instance.NewGame();
            SceneManager.LoadSceneAsync(0);
        }

        public void OnContinueClick()
        {
            DisableButtons();
            SerializationManager.Instance.LoadGame();
            SceneManager.LoadSceneAsync(SerializationManager.Instance.LastScene());
        }

        private void DisableButtons()
        {
            newGameButton.interactable = false;
            continueButton.interactable = false;
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}