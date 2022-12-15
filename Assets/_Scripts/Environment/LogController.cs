using _Scripts.Utility;
using _Scripts.Utility.Serialization;
using FMODUnity;
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
            SoundManager.PlayOneShot(FMODEvents.Instance.LogPickup, transform.position);
            Player.Instance.logCount++;
            Destroy(gameObject);
        }
    }
}