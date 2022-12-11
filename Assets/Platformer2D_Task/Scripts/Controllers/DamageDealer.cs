using System;
using UnityEngine;


namespace Platformer2D_Task
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class DamageDealer : MonoBehaviour
    {
        [SerializeField][Range(0, 100)]private int _damageValue = 3;

        private Type[] _damageTakers = { typeof(Player)};

        #region Collisions And Triggers Check

        private void OnCollisionEnter2D(Collision2D collision)
        {
            PerformDamageLogic(collision.gameObject);
        }

        void OnCollisionStay2D(Collision2D collision)
        {
            PerformDamageLogic(collision.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            PerformDamageLogic(collider.gameObject);
        }

        void OnTriggerStay2D(Collider2D collider)
        {
            PerformDamageLogic(collider.gameObject);
        }

        #endregion Collisions And Triggers Check

        private void PerformDamageLogic(GameObject target)
        {
            if (CheckDamageTaker(target, out Health health))
            {
                health.TakeDamage(_damageValue);
            }
        }

        private bool CheckDamageTaker(GameObject gameObject, out Health health)
        {
            var hasHealth = gameObject.TryGetComponent<Health>(out health);
            var isDamageTaker = false;

            foreach (var type in _damageTakers)
            {
                if (gameObject.TryGetComponent(type, out Component component))
                {
                    isDamageTaker = true;
                    break;
                }
            }

            return hasHealth && isDamageTaker;
        }
    }
}