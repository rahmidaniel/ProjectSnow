using System;
using _Scripts.Units.Utility;
using Scenes.Sctips.Checks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Units.Capabilities
{
    [RequireComponent(typeof(Ground), typeof(Rigidbody2D), typeof(Animator))]
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
        private Animator _animator;
        private static readonly int Speed = Animator.StringToHash("speed");

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _ground = GetComponent<Ground>();
        }

        private void Update()
        {
            if (blocked)
            {
                _body.velocity = Vector2.zero;
                return;
            }
            _desiredVelocity = new Vector2(_horizontal * Mathf.Max(maxSpeed - _ground.Friction, 0f) * speedModifier, 0f);
            _animator.SetFloat(Speed, Mathf.Abs(_desiredVelocity.x));
            switch (_desiredVelocity.x)
            {
                case < 0 when !_mFacingRight:
                case > 0 when _mFacingRight:
                    Flip();
                    break;
            }
        }

        private void FixedUpdate()
        {
            var velocity = _body.velocity;

            var acceleration = _ground.OnGround ? maxAcceleration : maxAirAcceleration;
            var maxSpeedChange = acceleration * Time.deltaTime;
            velocity.x = Mathf.MoveTowards(velocity.x, _desiredVelocity.x, maxSpeedChange);

            _body.velocity = velocity;
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