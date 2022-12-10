using System;
using _Scripts.Utility;
using TMPro;
using UnityEngine;

namespace _Scripts.Units.Utility
{
    public class InteractListener : MonoBehaviour
    {
        [SerializeField] private GameObject actionPanel;
        private TextMeshProUGUI _text;
        private bool _hasText;
        private void Start()
        {
            _text = actionPanel.GetComponentInChildren<TextMeshProUGUI>();
            Interactable.ShowMessage += ShowMessage;
            Interactable.HideMessage += HideMessage;
        }

        private void HideMessage()
        {
            actionPanel.SetActive(false);
        }

        private void ShowMessage(string text)
        {
            actionPanel.SetActive(true);
            if(_text != null) _text.text = text;
        }

        private void OnDestroy()
        {
            Interactable.ShowMessage -= ShowMessage;
            Interactable.HideMessage -= HideMessage;
        }
    }
}