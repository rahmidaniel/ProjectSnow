using _Scripts.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Environment
{
    public class LogController : Interactable
    {
        protected override string UpdateMessage()
        {
            return $"Press '{CurrentBinding}' to pick up log.";
        }

        protected override void Interact()
        {
            Player.Instance.logCount++;
            Destroy(gameObject);
        }
    }
}