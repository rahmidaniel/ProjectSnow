using _Scripts.Environment;
using UnityEngine;

namespace _Scripts.Units.Capabilities
{
    public class Lantern : MonoBehaviour
    {
        [SerializeField] private GameObject lantern;

        private void Start()
        {
            TimeController.OnDaytime += OnDusk;
        }

        private void OnDestroy()
        {
            TimeController.OnDaytime -= OnDusk;
        }

        private void OnDusk(bool daytime)
        {
            lantern.SetActive(!daytime);
        }
    }
}