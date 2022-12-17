using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platformer2D_Task
{    
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerMovement : MonoBehaviour
    {
        private const float JumpForce = 6.5f;
        private const string JumpButton = "Jump";

        public event Action<PlayerStates> StateChanged;

        [SerializeField] private float _moveSpeed = 5;
        
        private Rigidbody2D _rigidbody;
        private BoxCollider2D _boxCollider;
        private Health _health;
        private SpriteRenderer _renderer;
        private PlayerStates _state = PlayerStates.Idle;
        
        private bool _jumpKeyPressed;
        private bool _jumpEnable;

        public PlayerStates State
        {
            get => _state;
            private set 
            {
                _state = value;
                StateChanged?.Invoke(_state);
            }
        }

        private bool Dead => _health.Value <= Health.MinValue;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _health = GetComponent<Health>();
            _boxCollider = GetComponent<BoxCollider2D>();
            _renderer = GetComponent<SpriteRenderer>();

            _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            _health.MinValueReached += (health, value) => State = PlayerStates.Dead;
        }

        private void Update()
        {
            if (!_jumpKeyPressed && _jumpEnable)
            {
                _jumpKeyPressed = Input.GetButtonDown(JumpButton);
            }
        }

        private void FixedUpdate()
        {
            ResetHorizontalSpeed();

            if (Dead)
            {
                return;
            }
            
            PerformHorizontalMove();
            PerformJumpLogic();
        }

        private void ResetHorizontalSpeed()
        {
            var velocity = _rigidbody.velocity;
            velocity.x = 0;
            _rigidbody.velocity = velocity;
        }

        private void PerformHorizontalMove()
        {
            var moveHorizontal = Input.GetAxis(InputAxes.Horizontal);

            var movement = new Vector2(moveHorizontal, 0);

            _rigidbody.AddForce(movement * _moveSpeed, ForceMode2D.Impulse);

            SetRendererTurn(movement);

            if (State != PlayerStates.Jump)
            {
                State = movement == Vector2.zero ? 
                    PlayerStates.Idle : 
                    PlayerStates.Run;                
            }
        }

        private void SetRendererTurn(Vector2 movement)
        {
            var horizontal = movement.x;

            if (horizontal == 0)
            {
                return;
            }

            _renderer.flipX = horizontal > 0 ? false : true;
        }

        private void PerformJumpLogic()
        {
            if (_jumpKeyPressed && _jumpEnable)
            {
                _jumpKeyPressed = false;
                _jumpEnable = false;

                _rigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);

                State = PlayerStates.Jump;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            TryEnableJump(collision);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            TryEnableJump(collision);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            _jumpEnable = false;
            
            State = PlayerStates.Jump;
        }

        private void TryEnableJump(Collision2D collision)
        {
            if (CheckTheFloor(collision))
            {
                _jumpEnable = true;

                if (State == PlayerStates.Jump)
                {
                    State = PlayerStates.Idle;
                }
            }
        }

        private bool CheckTheFloor(Collision2D collision)
        {
            const float delta = 0.05f;

            var bounds = _boxCollider.bounds;
            var bottom = bounds.center.y - bounds.extents.y - _boxCollider.edgeRadius;

            var contacts = new List<ContactPoint2D>();
            collision.GetContacts(contacts);

            return contacts.All(contact => Mathf.Abs(contact.point.y - bottom) <= delta);
        }
    }
}