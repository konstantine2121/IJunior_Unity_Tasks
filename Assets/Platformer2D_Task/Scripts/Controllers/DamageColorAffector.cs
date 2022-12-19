using UnityEngine;

namespace Platformer2D_Task
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class DamageColorAffector : MonoBehaviour
    {
        [SerializeField]private Color _affectedColor;

        private Health _health;
        private SpriteRenderer _renderer;

        private Color _defaultColor;

        private void Awake()
        {
            _health = GetComponent<Health>();
            _renderer = GetComponent<SpriteRenderer>();

            _defaultColor = _renderer.color;
        }

        private void OnEnable()
        {
            _health.InvulnerabilityChanged += InvulnerabilityChanged;
        }

        private void OnDisable()
        {
            _health.InvulnerabilityChanged -= InvulnerabilityChanged;
        }

        private void InvulnerabilityChanged(Health health, bool invulnerability)
        {
            _renderer.color = invulnerability ?
                _affectedColor :
                _defaultColor;
        }
    }
}