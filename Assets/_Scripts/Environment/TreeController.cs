using _Scripts.Utility;
using UnityEngine;

namespace _Scripts.Environment
{
    public class TreeController : Interactable
    {
        [SerializeField] private GameObject drop;
        [SerializeField] private int dropCount = 2;
        private Vector3 _spread;

        [SerializeField] private bool pickUpAllowed = false;
        [SerializeField] private bool grabAllowed = false;
        [SerializeField] private bool hitAllowed = true;
        
        public Rigidbody2D Rigidbody2D { get; private set; }
        public Collider2D Collider2D { get; private set; }

        private new void Start()
        {
            base.Start();

            Rigidbody2D = GetComponent<Rigidbody2D>();
            Collider2D = GetComponent<Collider2D>();
            _spread = Collider2D.transform.localScale;
        }
            
        protected override string UpdateMessage()
        {
            if(hitAllowed)
                return "Press 'F' to cut tree.";
            if(grabAllowed)
                return "Press 'G' to grab log.";
            if (pickUpAllowed)
                return "Press 'F' to pick up log.";
            
            return "Log Interaction Error";
        }

        protected override void OnPlayerEnter()
        {
            if(grabAllowed) GameManager.Instance.grabObjects.Add(this);
        }

        protected override void OnPlayerExit()
        {
            if(grabAllowed) GameManager.Instance.grabObjects.Remove(this);
        }
        
        protected override void Interact()
        {
            if(hitAllowed) Hit();
            // else if(grabAllowed) Grab();
            else if (pickUpAllowed) PickUp();
        }

        private void Hit()
        {
            // get root point (y) of the tree
            var basePoint = transform.position.y - _spread.y / 2f; 
            
            for (int i = dropCount; i > 0; i--)
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

        private void PickUp()
        {
            GameManager.Instance.logCount++;
            Destroy(gameObject);
        }
    }
}