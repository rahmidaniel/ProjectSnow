using _Scripts.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Environment
{
    public class TreeController : Interactable
    {
        [SerializeField] private GameObject drop;
        [SerializeField] private int dropCount = 2;
        private Vector3 _spread;

        public Rigidbody2D Rigidbody2D { get; private set; }
        public Collider2D Collider2D { get; private set; }

        private void Start()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            Collider2D = GetComponent<Collider2D>();
            _spread = Collider2D.transform.localScale;
        }
            
        protected override string UpdateMessage()
        {
            return ($"Press '{CurrentBinding}' to cut tree.");
        }

        protected override void Interact()
        {
            // get root point (y) of the tree
            var basePoint = transform.position.y - _spread.y / 2f; 
            
            for (var i = dropCount; i > 0; i--)
            {
                var go = Instantiate(drop, transform.parent, true);
                
                // should spawn like a chain of objects
                var insertHeight = basePoint + go.transform.localScale.y * (i + 0.5f);
                
                // tree position
                Vector2 pos = transform.position;
                pos.y = insertHeight;
                
                // set correct position
                go.transform.position = pos;
            }

            Destroy(gameObject);
        }
    }
}