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
            SerializationManager.Instance.LoadGame();
            SetFirstSelected();
        }

        public void OnNewGameClick()
        {
            DisableButtons();
            SerializationManager.Instance.NewGame();
            SceneManager.LoadSceneAsync(1);
        }

        public void OnContinueClick()
        {
            DisableButtons();
            //SerializationManager.Instance.SaveGame();
            SerializationManager.Instance.LoadGame();
            SceneManager.LoadSceneAsync(SerializationManager.Instance.LastScene());
        }

        private void DisableButtons()
        {
            newGameButton.interactable = false;
            continueButton.interactable = false;
        }

        private void SetFirstSelected()
        {
            if (SerializationManager.Instance.HasData())
            {
                continueButton.interactable = true;
                continueButton.Select();
            }
            else
            {
                continueButton.interactable = false;
                newGameButton.Select();
            }
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}