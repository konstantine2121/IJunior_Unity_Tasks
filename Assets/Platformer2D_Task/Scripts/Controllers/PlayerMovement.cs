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
        private const int CollidersToCheckNumber = 128;
        private const float JumpForce = 6.5f;
        private const string JumpButton = "Jump";

        public event Action<PlayerState> StateChanged;

        [SerializeField] private float _moveSpeed = 5;
        
        private Rigidbody2D _rigidbody;
        private BoxCollider2D _boxCollider;
        private Health _health;
        private SpriteRenderer _renderer;
        private PlayerState _state = PlayerState.Idle;
        
        private bool _jumpKeyPressed;
        private bool _jumpEnable;

        public PlayerState State
        {
            get => _state;
            private set 
            {
                _state = value;
                StateChanged?.Invoke(_state);
            }
        }

        private bool Dead => _health.Value <= Health.MinValue;

        // Start is called before the first frame update
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _health = GetComponent<Health>();
            _boxCollider = GetComponent<BoxCollider2D>();
            _renderer = GetComponent<SpriteRenderer>();

            _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            _health.MinValueReached += (health, value) => State = PlayerState.Dead;
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

            if (State != PlayerState.Jump)
            {
                State = movement == Vector2.zero ? 
                    PlayerState.Idle : 
                    PlayerState.Run;                
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

                State = PlayerState.Jump;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (CheckTheFloor(collision))
            {
                _jumpEnable = true;
                State = PlayerState.Idle;
            }
        }
        
        private void OnCollisionExit2D(Collision2D collision)
        {
            _jumpEnable = false;

            var colliders = new ContactPoint2D[CollidersToCheckNumber];
            var number = _rigidbody.GetContacts(colliders);//Ётот подход не работает((
            //ѕерса как будто бы подкидывает в воздух и он никаких коллайдеров не касаетс€.

            if (number == 0)
            {
                State = PlayerState.Jump;
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