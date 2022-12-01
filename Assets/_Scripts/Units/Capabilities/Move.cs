using System;
using JetBrains.Annotations;
using Scenes.Sctips.Checks;
using Scenes.Sctips.Controllers;
using UnityEngine;

[RequireComponent(typeof(Controller))]
public class Move : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 4f;
    [SerializeField] private float maxAcceleration = 35f;
    [SerializeField] private float maxAirAcceleration = 20f;
    public bool blocked;

    private Controller _controller;
    private Vector2 _direction, _desiredVelocity, _velocity;
    private Rigidbody2D _body;
    private Ground _ground;

    private float _maxSpeedChange, _acceleration;
    private bool _onGround;

    private SpriteRenderer _sprite;
    private bool _mFacingRight;
    private Animator _animator;
    private static readonly int Speed = Animator.StringToHash("speed");

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _body = GetComponent<Rigidbody2D>();
        _ground = GetComponent<Ground>();
        _controller = GetComponent<Controller>();

        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (blocked) return;
        _direction.x = _controller.input.RetrieveMoveInput();
        _desiredVelocity = new Vector2(_direction.x, 0f) * Mathf.Max(maxSpeed - _ground.Friction, 0f);
    }

    private void FixedUpdate()
    {
        _onGround = _ground.OnGround;
        _velocity = _body.velocity;

        _acceleration = _onGround ? maxAcceleration : maxAirAcceleration;
        _maxSpeedChange = _acceleration * Time.deltaTime;
        _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, _maxSpeedChange);
        
        
        // Animations and sprite handling
        _animator.SetFloat(Speed, Mathf.Abs(_velocity.x));
        switch (_velocity.x)
        {
            case < 0 when !_mFacingRight:
            case > 0 when _mFacingRight:
                Flip();
                break;
        }
        
        
        _body.velocity = _velocity;
    }
    
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        _mFacingRight = !_mFacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}