using UnityEngine;
using UnityEngine.UIElements;

namespace Platformer2D_Task
{
    [RequireComponent(typeof(UIDocument))]
    public class PlayerHPBar : MonoBehaviour
    {
        [SerializeField] private Health _health;

        private UIDocument _ui;
        private VisualElement _healthBar;

        public void RegisterHealth(Health health)
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
            _ui = GetComponent<UIDocument>();
            _healthBar = _ui.rootVisualElement.Q<VisualElement>("health-bar");
        }

        private void OnEnable()
        {
            RegisterHealth(_health);
        }

        private void OnDisable()
        {
            UnregisterHealth();
        }

        private void UpdateHP(Health health, float value)
        {
            if (health == null)
            {
                return;
            }

            var maxHealth = health.MaxValue;
            var percentage = value / maxHealth * 100;

            _healthBar.style.width = new Length(percentage, LengthUnit.Percent);
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