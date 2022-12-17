//using System;
using System.Linq;
using UnityEngine;


namespace Platformer2D_Task
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class DamageDealer : MonoBehaviour
    {
        [SerializeField]private DamageTargetType [] _damageTakersWhiteList;
        [SerializeField][Range(0, 100)]private int _damageValue = 3;

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
            if (CheckDamageTaker(target, out IDamageTaker damageTaker))
            {
                damageTaker.TakeDamage(_damageValue);
            }
        }

        private bool CheckDamageTaker(GameObject gameObject, out IDamageTaker damageTaker)
        {
            var damageTargetType = gameObject.GetDamageTargetType();
            var isDamageable = gameObject.TryGetComponent(out damageTaker) && 
                (damageTargetType != DamageTargetType.None);
            
            if (isDamageable == false)
            {
                return false;
            }

            return _damageTakersWhiteList.Contains(damageTargetType);
        }
    }
}