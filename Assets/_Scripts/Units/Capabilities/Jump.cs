using System;
using System.Security.Cryptography.X509Certificates;
using _Scripts.Environment;
using _Scripts.Units.Capabilities;
using _Scripts.Units.Utility;
using _Scripts.Utility;
using FMOD.Studio;
using Scenes.Sctips.Checks;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

namespace Scenes.Sctips.Capabilities
{
    [RequireComponent(typeof(Ground), typeof(Rigidbody2D), typeof(PlayerAnimator))]
    public class Jump : MonoBehaviour
    {
        [SerializeField, Range(0f, 10f)] private float jumpHeight = 3f;
        [SerializeField, Range(0f, 5f)] private float downwardMovementMultiplier = 3f;
        [SerializeField, Range(0f, 5f)] private float upwardMovementMultiplier = 1.7f;
        
        private Rigidbody2D _body;
        private Ground _ground;
        private Vector2 _velocity;

        private bool _desiredJump;
        private bool _jumpStarted;

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _ground = GetComponent<Ground>();
        }

        public void OnJump(InputValue value)
        {
            if (_ground.OnGround && Player.Instance.CanMove())
            {
                _desiredJump = true;
            }
        }

        private void FixedUpdate()
        {
            _velocity = _body.velocity;
            
            // Jump
            if (_desiredJump)
            {
                _desiredJump = false;
                CalculateJump();
            }

            switch (_body.velocity.y)
            {
                case > 0.01f:
                    _body.gravityScale = upwardMovementMultiplier;
                    if(!_ground.OnGround) PlayerAnimator.Instance.ChangeAnimation(PlayerAnimationState.Jump);
                    break;
                case < -0.01f:
                    _body.gravityScale = downwardMovementMultiplier;
                    if(!_ground.OnGround) PlayerAnimator.Instance.ChangeAnimation(PlayerAnimationState.Fall);
                    break;
                case > -0.1f and < 0f:
                    if(!_ground.OnGround) PlayerAnimator.Instance.JumpSound(1);
                    break;
                case 0f:
                    _body.gravityScale = 1f;
                    break;
                default:
                    _body.gravityScale = _body.gravityScale;
                    break;
            }
            
            _body.velocity = _velocity;
        }

        private void CalculateJump()
        {
            if (!_ground.OnGround) return;
            
            var jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight);
                
            switch (_velocity.y)
            {
                case > 0f:
                    jumpSpeed = Mathf.Max(jumpSpeed - _velocity.y, 0f);
                    break;
                case < 0f:
                    jumpSpeed += Mathf.Abs(_body.velocity.y);
                    break;
            }
            _velocity.y += jumpSpeed;
        }
    }
}