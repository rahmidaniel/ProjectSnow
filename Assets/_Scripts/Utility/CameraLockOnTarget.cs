using System;
using _Scripts.Environment;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.Utility
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraLockOnTarget : MonoBehaviour
    {
        private void OnEnable()
        {
            var vcam = GetComponent<CinemachineVirtualCamera>();
            vcam.Follow = Player.Instance.transform;
        }
    }
}