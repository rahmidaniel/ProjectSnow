using System.IO.Compression;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace _Scripts.Environment
{
    public class Parallax : MonoBehaviour
    {
        [SerializeField] private Vector2 parallaxEffectMultiplier;
        public Camera cam;
        private Transform cameraTransform;
        private Vector3 lastCameraPosition;
        
        private float textureUnitSizeX, textureUnitSizeY;

        private void Start()
        {
            //cam = Camera.main;
            cameraTransform = cam.transform;
            lastCameraPosition = Vector2.zero;
            
            var spriteRenderer = GetComponent<SpriteRenderer>();
            var texture = spriteRenderer.sprite.texture;
            textureUnitSizeX = texture.width / spriteRenderer.sprite.pixelsPerUnit * transform.localScale.x;
            textureUnitSizeY = texture.height / spriteRenderer.sprite.pixelsPerUnit * transform.localScale.y;

            spriteRenderer.drawMode = SpriteDrawMode.Tiled;
            spriteRenderer.size = new Vector2(spriteRenderer.size.x * 3, spriteRenderer.size.y);
        }

        private void FixedUpdate()
            {
                var position = cameraTransform.position;
                
                var deltaMovement = (position - lastCameraPosition) / transform.localScale.x; // scale offset (magnitude)
                transform.position -= new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);
                lastCameraPosition = position;

                if (!(Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)) return;
                
                var offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
                transform.position = new Vector3 (cameraTransform.position.x + offsetPositionX, transform.position.y);
            }
        }
}