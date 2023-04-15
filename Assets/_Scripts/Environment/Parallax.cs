using UnityEngine;

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
            var sprite = spriteRenderer.sprite;
            var localScale = transform.localScale;
            var texture = sprite.texture;
            textureUnitSizeX = texture.width / sprite.pixelsPerUnit * localScale.x;
            textureUnitSizeY = texture.height / sprite.pixelsPerUnit * localScale.y;

            spriteRenderer.drawMode = SpriteDrawMode.Tiled;
            spriteRenderer.size = new Vector2(spriteRenderer.size.x * 3, spriteRenderer.size.y);
        }

        private void FixedUpdate()
        {
            var position = cameraTransform.position;

            var deltaMovement = (position - lastCameraPosition) / transform.localScale.x; // scale offset (magnitude)
            transform.position -= new Vector3(deltaMovement.x * parallaxEffectMultiplier.x,
                deltaMovement.y * parallaxEffectMultiplier.y);
            lastCameraPosition = position;

            if (!(Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)) return;

            var offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y);
        }
    }
}