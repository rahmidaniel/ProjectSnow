using _Scripts.Units;
using _Scripts.Units.Capabilities;
using _Scripts.Utility;
using Scenes.Sctips.Checks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scenes.Sctips.Capabilities
{
    [RequireComponent(typeof(Ground), typeof(Rigidbody2D), typeof(PlayerAnimator))]
    public class Jump : MonoBehaviour
    {
        [SerializeField] [Range(0f, 10f)] private float jumpHeight = 3f;
        [SerializeField] [Range(0f, 5f)] private float downwardMovementMultiplier = 3f;
        [SerializeField] [Range(0f, 5f)] private float upwardMovementMultiplier = 1.7f;

        private Rigidbody2D _body;

        private bool _desiredJump;
        private Ground _ground;
        private bool _jumpStarted;
        private Vector2 _velocity;

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _ground = GetComponent<Ground>();
        }

        private void FixedUpdate()
        {
            _velocity = _body.velocity;

            // Jump
            if (_desiredJump)
            {
                _desiredJump = false;
                CalculateJump();
                _jumpStarted = true;
            }

            switch (_body.velocity.y)
            {
                case > 0.01f:
                    _body.gravityScale = upwardMovementMultiplier;
                    if (!_ground.OnGround) PlayerAnimator.Instance.ChangeAnimation(PlayerAnimationState.Jump);
                    break;
                case < -0.01f:
                    _body.gravityScale = downwardMovementMultiplier;
                    if (!_ground.OnGround)
                    {
                        PlayerAnimator.Instance.ChangeAnimation(PlayerAnimationState.Fall);
                    }
                    else
                    {
                        if (_jumpStarted)
                        {
                            PlayerAnimator.Instance.JumpSound(1);
                            _jumpStarted = false;
                        }
                    }

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

        public void OnJump(InputValue value)
        {
            if (_ground.OnGround && Player.Instance.CanMove()) _desiredJump = true;
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