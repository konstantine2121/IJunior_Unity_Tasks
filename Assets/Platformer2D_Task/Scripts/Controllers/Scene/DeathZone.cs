using UnityEngine;

namespace Platformer2D_Task
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class DeathZone : MonoBehaviour
    {
        private BoxCollider2D _boxCollider;

        private void Awake()
        {
            _boxCollider = GetComponent<BoxCollider2D>();            
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var collisionObject = collision.gameObject;

            if (collisionObject.TryGetComponent<IDamageTaker>(out IDamageTaker damageTaker))
            {
                damageTaker.Kill();
            }

            if (collisionObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidbody))
            {
                rigidbody.simulated = false;
            }

            if (collisionObject.TryGetComponent<ICollectable>(out ICollectable collectable))
            {
                Destroy(collisionObject);
            }
        }
    }
}