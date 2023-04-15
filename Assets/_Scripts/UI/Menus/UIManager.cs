using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.UI
{
    public enum UIState
    {
        HUD,
        PauseMenu,
        DeathMenu
    }

    public class UIManager : MonoBehaviour
    {
        public static UnityAction<UIState> UIStateChanged;
        [SerializeField] private GameObject HUD;
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject deathMenu;

        private void OnEnable()
        {
            UIStateChanged += OnUIStateChanged;
            UIStateChanged.Invoke(UIState.HUD);
        }

        private void OnDisable()
        {
            UIStateChanged -= OnUIStateChanged;
        }

        private void OnUIStateChanged(UIState newState)
        {
            HUD.SetActive(newState == UIState.HUD);
            pauseMenu.SetActive(newState == UIState.PauseMenu);
            deathMenu.SetActive(newState == UIState.DeathMenu);
        }
    }
}