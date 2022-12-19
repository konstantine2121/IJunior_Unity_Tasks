using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platformer2D_Task
{    
    [RequireComponent(typeof(Gundam))]    
    [RequireComponent(typeof(BoxCollider2D))]    
    [RequireComponent(typeof(SpriteRenderer))]
    public class GundamPatrol : MonoBehaviour
    {   
        public event Action<GundamStates> StateChanged;

        [SerializeField] private Vector3[] _wayPoints;
        [SerializeField] private float _moveSpeed = 5;
        [SerializeField] private float _stayOnPointTime = 1;

        private BoxCollider2D _boxCollider;
        private SpriteRenderer _renderer;

        private bool _stayOnPoint = false;
        private WaitForSeconds _stayOnPointDelay;
        private int _wayPoinIndex = 0;
        private GundamStates _state = GundamStates.Idle;

        public GundamStates State
        {
            get => _state;
            private set
            {
                _state = value;
                StateChanged?.Invoke(_state);
            }
        }

        public void SetNewWayPoints(IEnumerable<Vector3> points)
        {
            _wayPoints = points.ToArray();
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
            if (_wayPoints.Length == 0 || _stayOnPoint)
            {
                return;
            }

            var previousPosition = transform.position;
            var targetPoint = _wayPoints[_wayPoinIndex];
            transform.position = Vector2.MoveTowards(transform.position, targetPoint, _moveSpeed * Time.fixedDeltaTime);
            var delta = transform.position - previousPosition;
            State = delta.x != 0 ? GundamStates.Walk : GundamStates.Idle;
            SetRendererTurn(delta);

            if (transform.position == targetPoint)
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
            State = GundamStates.Idle;
            StartCoroutine(EndStayMode());
        }

        private IEnumerator EndStayMode()
        {
            yield return _stayOnPointDelay;
            _stayOnPoint = false;
        }
    }
}