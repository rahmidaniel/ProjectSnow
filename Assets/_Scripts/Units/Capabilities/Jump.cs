using _Scripts.Environment;
using _Scripts.Units.Utility;
using _Scripts.Utility;
using Scenes.Sctips.Checks;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

namespace Scenes.Sctips.Capabilities
{
    [RequireComponent(typeof(Ground), typeof(Rigidbody2D))]
    public class Jump : MonoBehaviour
    {
        [SerializeField, Range(0f, 10f)] private float jumpHeight = 3f;
        [SerializeField, Range(0f, 5f)] private float downwardMovementMultiplier = 3f;
        [SerializeField, Range(0f, 5f)] private float upwardMovementMultiplier = 1.7f;
        
        private Rigidbody2D _body;
        private Ground _ground;
        private Vector2 _velocity;

        private bool _desiredJump;

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
               
            _body.gravityScale = _body.velocity.y switch
            {
                > 0 => upwardMovementMultiplier,
                < 0 => downwardMovementMultiplier,
                0 => 1f,
                _ => _body.gravityScale
            };

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