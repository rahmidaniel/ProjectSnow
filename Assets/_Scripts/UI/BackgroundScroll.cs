using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Units.Utility
{
    public class BackgroundScroll : MonoBehaviour
    {
        [SerializeField] private Vector2 direction;
        private RawImage _rawImage;

        private void Start()
        {
            _rawImage = GetComponent<RawImage>();
        }

        private void Update()
        {
            _rawImage.uvRect = new Rect(Time.deltaTime * direction + _rawImage.uvRect.position, _rawImage.uvRect.size);
        }
    }
}