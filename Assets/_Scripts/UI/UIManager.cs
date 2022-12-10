using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using _Scripts.Units.Utility;

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
        [SerializeField] private GameObject HUD;
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject deathMenu;
        
        public static UnityAction<UIState> UIStateChanged;

        private void OnEnable()
        {
            UIStateChanged += OnUIStateChanged;
            UIStateChanged.Invoke(UIState.HUD);
        }

        private void OnUIStateChanged(UIState newState)
        {
            HUD.SetActive(newState == UIState.HUD);
            pauseMenu.SetActive(newState == UIState.PauseMenu);
            deathMenu.SetActive(newState == UIState.DeathMenu);
        }

        private void OnDisable()
        {
            UIStateChanged -= OnUIStateChanged;
        }
    }
}