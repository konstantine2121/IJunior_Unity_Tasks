using UnityEngine;

namespace Platformer2D_Task
{
    public class PlayerHP : MonoBehaviour
    {
        [SerializeField] private Health _health;

        public void RegisterHealth(Health health)
        {
            UnregisterHealth();

            if (health != null)
            {
                _health = health;
                _health.ValueChanged += UpdateHP;
            }
        }

        public void UnregisterHealth()
        {
            if (_health != null)
            {
                _health.ValueChanged -= UpdateHP;
            }

            _health = null;
        }

        private void UpdateHP(Health health, int value)
        {

        }

        private void OnValidate()
        {
            if (_health != null)
            {
                RegisterHealth(_health);
            }
            else
            {
                UnregisterHealth();
            }
        }
    }
}