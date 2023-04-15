using _Scripts.Utility;
using TMPro;
using UnityEngine;

namespace _Scripts.UI
{
    public class InteractListener : MonoBehaviour
    {
        [SerializeField] private GameObject actionPanel;
        private bool _hasText;
        private TextMeshProUGUI _text;

        private void Start()
        {
            _text = actionPanel.GetComponentInChildren<TextMeshProUGUI>();
            Interactable.ShowMessage += ShowMessage;
            Interactable.HideMessage += HideMessage;
        }

        private void OnDestroy()
        {
            Interactable.ShowMessage -= ShowMessage;
            Interactable.HideMessage -= HideMessage;
        }

        private void HideMessage()
        {
            actionPanel.SetActive(false);
        }

        private void ShowMessage(string text)
        {
            actionPanel.SetActive(true);
            if (_text != null) _text.text = text;
        }
    }
}