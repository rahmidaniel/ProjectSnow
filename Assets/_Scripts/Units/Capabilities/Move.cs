using _Scripts.Utility;
using FMOD.Studio;
using Scenes.Sctips.Checks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Units.Capabilities
{
    [RequireComponent(typeof(Ground), typeof(Rigidbody2D), typeof(PlayerAnimator))]
    public class Move : MonoBehaviour
    {
        [SerializeField] private float maxSpeed = 15f;
        [SerializeField] private float maxAcceleration = 20f;
        [SerializeField] private float maxAirAcceleration = 18f;
        public float speedModifier = 1f; // For snow slow effect
        public bool blocked;

        private Vector2 _desiredVelocity;
        private Ground _ground;
        
        private float _horizontal;
        private Rigidbody2D _body;

        private bool _mFacingRight;

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _ground = GetComponent<Ground>();
        }

        private void Update()
        {
            _desiredVelocity = new Vector2(_horizontal * Mathf.Max(maxSpeed - _ground.Friction, 0f) * speedModifier, 0f);
        }

        private void FixedUpdate()
        {
            var velocity = Vector2.zero;

            if (!blocked && !Attack.Attacking)
            {
                velocity = _body.velocity;
                var acceleration = _ground.OnGround ? maxAcceleration : maxAirAcceleration;
                var maxSpeedChange = acceleration * Time.deltaTime;
                velocity.x = Mathf.MoveTowards(velocity.x, _desiredVelocity.x, maxSpeedChange);    
            }

            _body.velocity = velocity;
            UpdateAnimation();
        }

        private void UpdateAnimation()
        {
            if (_ground.OnGround && !Attack.Attacking)
                PlayerAnimator.Instance.ChangeAnimation(_body.velocity.x == 0f ? PlayerAnimationState.Idle : PlayerAnimationState.Run);

            switch (_body.velocity.x)
            {
                case < 0 when !_mFacingRight:
                case > 0 when _mFacingRight:
                    Flip();
                    break;
            }
        }

        public void OnMove(InputValue value)
        {
            _horizontal = value.Get<Vector2>().x;
        }

        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            _mFacingRight = !_mFacingRight;

            // Multiply the player's x local scale by -1.
            var scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}