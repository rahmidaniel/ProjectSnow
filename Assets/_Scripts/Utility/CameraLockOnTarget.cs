using _Scripts.Units;
using Cinemachine;
using UnityEngine;

namespace _Scripts.Utility
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraLockOnTarget : MonoBehaviour
    {
        private void Start()
        {
            var vcam = GetComponent<CinemachineVirtualCamera>();
            vcam.Follow = Player.Instance.transform;
        }
    }
}