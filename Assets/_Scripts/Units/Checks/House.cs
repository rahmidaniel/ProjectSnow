using _Scripts.Utility;
using UnityEngine;

namespace _Scripts.Units.Checks
{
    public class House : MonoBehaviour
    {
        private DoorController _door;

        private void Start()
        {
            _door = GameObject.FindWithTag("Door").GetComponent<DoorController>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            Player.Instance.HouseInfo.State = _door.IsOpen ? HouseState.InsideOpen : HouseState.Inside;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            Player.Instance.HouseInfo.State = _door.IsOpen ? HouseState.OutsideOpen : HouseState.Outside;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            Player.Instance.HouseInfo.State = _door.IsOpen ? HouseState.InsideOpen : HouseState.Inside;
        }
    }
}