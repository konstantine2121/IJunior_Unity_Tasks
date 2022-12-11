using UnityEngine;

namespace Platformer2D_Task
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class DamageAffector : MonoBehaviour
    {
        [SerializeField]private Color _affectedColor;

        private Health _health;
        private SpriteRenderer _renderer;

        private Color _defaultColor;

        private void Awake()
        {
            _health = GetComponent<Health>();
            _renderer = GetComponent<SpriteRenderer>();

            _health.InvulnerabilityChanged += InvulnerabilityChanged;
            _defaultColor = _renderer.color;
        }

        private void InvulnerabilityChanged(Health health, bool invulnerability)
        {
            _renderer.color = invulnerability ?
                _affectedColor :
                _defaultColor;
        }
    }
}