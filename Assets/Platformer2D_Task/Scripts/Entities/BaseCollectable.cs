using UnityEngine;

namespace Platformer2D_Task
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class BaseCollectable : MonoBehaviour, ICollectable
    {
        protected const int MinNumberOfObjects = 1;

        [SerializeField] private int _numberOfObjects = 1;

        public abstract CollectableTypes CollectableType { get; }

        public int NumberOfObjects => _numberOfObjects;

        public void Take()
        {
            _numberOfObjects = 0;

            PerformAfterTake();
        }

        protected virtual void PerformAfterTake()
        {

        }

        private void OnValidate()
        {
            if (_numberOfObjects < MinNumberOfObjects)
            {
                _numberOfObjects = MinNumberOfObjects;
            }
        }
    }
}