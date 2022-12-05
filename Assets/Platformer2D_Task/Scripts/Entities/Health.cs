using System;
using UnityEngine;

namespace Platformer2D_Task
{
    public class Health : MonoBehaviour
    {
        public  const int MinValue = 0;

        public event Action<Health, int> ValueChanged;
        public event Action<Health, int> MinValueReached;

        [SerializeField][Range(1, 10)] int _maxValue;

        private int _value;

        public int Value => _value;

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            _value = _maxValue;
        }

        public void TakeDamage(int damage)
        {
            var currValue = _value;
            _value = (int) Mathf.MoveTowards(_value, MinValue, Mathf.Abs(damage));
            var valueChanged = currValue != _value;

            if (valueChanged)
            {
                ValueChanged?.Invoke(this, _value);
            }

            if (valueChanged && _value == MinValue)
            {
                MinValueReached?.Invoke(this, _value);
            }
        }

        public void Kill()
        {
            _value = MinValue;
            MinValueReached?.Invoke(this, _value);
        }
    }
}