using System;
using System.Collections;
using UnityEngine;

namespace Platformer2D_Task
{
    public class Health : MonoBehaviour, IHealth
    {
        public const float MinValue = 0;

        public event Action<IHealth, float> ValueChanged;
        public event Action<IHealth, float> Died;
        public event Action<IHealth, bool> InvulnerabilityChanged;

        [SerializeField] [Range(1, 10)] float _maxValue;
        [SerializeField] [Range(1, 5)] float _invulnerabilityTime = 3;

        private float _value;
        private bool _invulnerability;
        private WaitForSeconds _invulnerabilityDelay;

        public float MaxValue => _maxValue;

        public float Value
        {
            get
            {
                return _value;
            }
            private set
            {
                var valueChanged = _value != value;
                if (valueChanged == false)
                {
                    return;
                }

                var oldValue = _value;
                _value = value;

                ValueChanged?.Invoke(this, _value);

                if (_value == MinValue)
                {
                    Died?.Invoke(this, _value);
                }
                else
                {
                    if (_value < oldValue)
                    {
                        StartInvulnerability();
                    }
                }
            }
        }

        public bool Invulnerability
        {
            get
            {
                return _invulnerability;
            }
            private set
            {
                _invulnerability = value;
                InvulnerabilityChanged?.Invoke(this, _invulnerability);
            }
        }

        private void Awake()
        {
            Initialize();
            _invulnerabilityDelay = new WaitForSeconds(_invulnerabilityTime);
        }

        public void Initialize()
        {
            Value = _maxValue;
        }

        public void TakeDamage(float damage)
        {
            if (Invulnerability == false)
            {
                Value = (float)Mathf.MoveTowards(Value, MinValue, Mathf.Abs(damage));
            }
        }

        public void TakeHeal(float heal)
        {
            Value = (float)Mathf.MoveTowards(Value, MaxValue, Mathf.Abs(heal));
        }

        public void Kill()
        {
            Value = MinValue;
        }

        private void StartInvulnerability()
        {
            Invulnerability = true;
            StartCoroutine(EndInvulnerability());
        }

        private IEnumerator EndInvulnerability()
        {
            yield return _invulnerabilityDelay;
            Invulnerability = false;
        }
    }    
}