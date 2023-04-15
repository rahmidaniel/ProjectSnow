using _Scripts.Units;
using _Scripts.Utility;

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