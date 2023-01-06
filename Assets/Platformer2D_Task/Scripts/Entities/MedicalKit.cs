using UnityEngine;

namespace Platformer2D_Task
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class MedicalKit : BaseCollectable
    {
        public const float DefaultHealValue = 3;

        [SerializeField][Range(0,10)]private float _amountOfHeal = DefaultHealValue;

        private Rigidbody2D _rigidbody;
        private BoxCollider2D _collider;

        public override CollectableTypes CollectableType => CollectableTypes.MedicalKit;

        public float AmountOfHeal => _amountOfHeal;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<BoxCollider2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            ActivateTriggerMode();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out ICollectable collectable))
            {
                ActivateTriggerMode();
            }
        }

        private void ActivateTriggerMode()
        {
            _rigidbody.constraints = 
                RigidbodyConstraints2D.FreezePositionY | 
                RigidbodyConstraints2D.FreezePositionX | 
                RigidbodyConstraints2D.FreezeRotation;

            _collider.isTrigger = true;
        }

        protected override void PerformAfterTake()
        {
            Destroy(gameObject);
        }
    }
}