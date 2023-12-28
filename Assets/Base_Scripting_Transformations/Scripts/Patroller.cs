using UnityEngine;

namespace Assets.Base_Scripting_Transformations.Scripts
{
    //Невнимательно прочитал задание, поэтому этот скрипт можно не проверять.
    public class Patroller : MonoBehaviour
    {
        private const float MinTargetOffset = 0.00001f;
        [SerializeField] private Vector3 _targetPoint = Vector3.zero;
        [SerializeField][Range(0, 5)] private float _moveSpeed;

        private Vector3 _startPoint = Vector3.zero;
        private Direction _direction = Direction.ToTarget;

        private void Awake()
        {
            _startPoint = transform.position;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            if (!CheckDistance(_startPoint, _targetPoint))
            {
                return;
            }

            var position = transform.position;
            var distance = _moveSpeed * Time.deltaTime;
            var targetPoint = _direction == Direction.ToTarget ? 
                _targetPoint : 
                _startPoint;

            var maxDistance = Vector3.Distance(position, targetPoint);
            distance = Mathf.Min(distance, maxDistance);

            transform.Translate((targetPoint - position).normalized * distance);

            if (!CheckDistance(transform.position, targetPoint))
            {
                SwitchDirection();
            }
        }

        private bool CheckDistance(Vector3 a, Vector3 b)
        {
            return Vector3.Distance(a, b) > MinTargetOffset;
        }

        private void SwitchDirection()
        {
            _direction = _direction == Direction.ToTarget ?
                Direction.ToStart :
                Direction.ToTarget;
        }

        private enum Direction
        {
            ToTarget,
            ToStart
        }
    }
}