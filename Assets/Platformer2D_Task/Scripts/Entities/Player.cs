using UnityEngine;

namespace Platformer2D_Task
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(BoxCollector))]

    public class Player : MonoBehaviour, IDamageTargetTypeContainer
    {
        private Health _health;
        private BoxCollector _boxCollector;

        public DamageTargetTypes DamageTargetType => DamageTargetTypes.Player;

        public Health Health => _health;

        public BoxCollector BoxCollector => _boxCollector;

        private void Awake()
        {
            _health = GetComponent<Health>();
            _boxCollector= GetComponent<BoxCollector>();
        }
    }
}