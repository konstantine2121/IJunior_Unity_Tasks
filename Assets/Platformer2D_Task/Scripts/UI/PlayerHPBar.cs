using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace Platformer2D_Task.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class PlayerHpBar : MonoBehaviour, IPlayerHpBar
    {
        private const float CoroutineDelay = 0.05f;
        private const string HealthBarName = "health-bar";

        [SerializeField] private IHealth _health;

        private Coroutine _healthUpdater;
        private VisualElement _rootElement;
        private VisualElement _healthBar;
        private WaitForSeconds _delay = new WaitForSeconds(CoroutineDelay);
        private float _increaseRate = 4f;

        public void RegisterHealth(IHealth health)
        {
            UnregisterHealth();

            if (health != null)
            {
                _health = health;
                _health.ValueChanged += UpdateHP;

                UpdateHP(health, health.Value);
            }
        }

        public void UnregisterHealth()
        {
            if (_health != null)
            {
                _health.ValueChanged -= UpdateHP;
            }
        }

        private void Awake()
        {
            _rootElement = GetComponent<UIDocument>().rootVisualElement;
            _healthBar = _rootElement.Q<VisualElement>(HealthBarName);
        }

        private void OnEnable()
        {
            RegisterHealth(_health);
        }

        private void OnDisable()
        {
            UnregisterHealth();
        }

        private void UpdateHP(IHealth health, float value)
        {
            if (health == null)
            {
                return;
            }

            var maxHealth = health.MaxValue;
            var percentage = value / maxHealth * 100;

            if (_healthUpdater != null)
            {
                StopCoroutine(_healthUpdater);
            }

            _healthUpdater = StartCoroutine(ChangeHealthValue(percentage));
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

        private float GetHealthPercentage()
        {
            return _healthBar.style.width.value.value;
        }

        private IEnumerator ChangeHealthValue(float percentage)
        {
            do
            {
               _healthBar.style.width = new Length(
                   Mathf.MoveTowards(GetHealthPercentage(), percentage, _increaseRate),
                   LengthUnit.Percent);

                yield return _delay;
            }
            while (GetHealthPercentage() != percentage);

            _healthUpdater = null;
        }
    }
}