using _Scripts.Utility;
using UnityEngine;

namespace _Scripts.Units.Utility
{
    public class Dialogue : Interactable
    {
        [SerializeField] private string message;

        protected override string UpdateMessage()
        {
            return message;
        }

        protected override void Interact()
        {
        }
    }
}