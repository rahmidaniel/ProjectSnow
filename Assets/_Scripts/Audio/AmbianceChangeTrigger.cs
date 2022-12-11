using System;
using UnityEngine;

namespace _Scripts.Utility
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class AmbianceChangeTrigger : MonoBehaviour
    {
        [Header("Parameter on Enter")]
        [SerializeField] private string enterParameterName;
        [SerializeField] private float enterParameterValue;
        [Header("Parameter on Exit")]
        [SerializeField] private string exitParameterName;
        [SerializeField] private float exitParameterValue;
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag("Player")) return;
            SoundManager.Instance.SetAmbienceParameter(enterParameterName, enterParameterValue);
        }
        private void OnTriggerExit2D(Collider2D col)
        {
            if (!col.CompareTag("Player")) return;
            SoundManager.Instance.SetAmbienceParameter(exitParameterName, exitParameterValue);
        }
    }
}