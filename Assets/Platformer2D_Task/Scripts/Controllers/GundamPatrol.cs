using System;
using System.Collections;
using UnityEngine;

namespace Platformer2D_Task
{    
    [RequireComponent(typeof(Gundam))]    
    [RequireComponent(typeof(BoxCollider2D))]    
    [RequireComponent(typeof(SpriteRenderer))]
    public class GundamPatrol : MonoBehaviour
    {   
        public event Action<GundamState> StateChanged;

        [SerializeField] private Transform [] _wayPoints;
        [SerializeField] private float _moveSpeed = 5;
        [SerializeField] private float _stayOnPointTime = 1;
        
        private BoxCollider2D _boxCollider;        
        private SpriteRenderer _renderer;

        private bool _stayOnPoint = false;
        private WaitForSeconds _stayOnPointDelay;
        private int _wayPoinIndex = 0;
        private GundamState _state = GundamState.Idle;
        
        public GundamState State
        {
            get => _state;
            private set 
            {
                _state = value;
                StateChanged?.Invoke(_state);
            }
        }

        private void Awake()
        {
            _stayOnPointDelay = new WaitForSeconds(_stayOnPointTime);

            _boxCollider = GetComponent<BoxCollider2D>();
            _renderer = GetComponent<SpriteRenderer>();

            _boxCollider.isTrigger = true;
        }

        private void FixedUpdate()
        {
            PerformPatrolLogic();
        }

        private void PerformPatrolLogic()
        {
            if ( _wayPoints.Length == 0 || _stayOnPoint)
            {
                return;
            }

            var previousPosition = transform.position;
            var targetPoint = _wayPoints[_wayPoinIndex];
            transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, _moveSpeed*Time.fixedDeltaTime);
            var delta = transform.position - previousPosition;
            State = delta.x != 0 ? GundamState.Walk : GundamState.Idle;
            SetRendererTurn(delta);

            if (transform.position == targetPoint.position)
            {
                IncrementWayPointIndex();
                BeginStayMode();
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

        private void IncrementWayPointIndex()
        {
            _wayPoinIndex++;

            if (_wayPoinIndex >= _wayPoints.Length)
            {
                _wayPoinIndex = 0;
            }
        }

        private void BeginStayMode()
        {
            _stayOnPoint = true;
            State = GundamState.Idle;
            StartCoroutine(EndStayMode());
        }

        private IEnumerator EndStayMode()
        {
            yield return _stayOnPointDelay;
            _stayOnPoint = false;
        }
    }
}