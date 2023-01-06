using UnityEngine;

namespace Platformer2D_Task
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(BoxCollector))]

    public class Player : MonoBehaviour, IPlayer
    {
        private IHealth _health;
        private BoxCollector _boxCollector;
        private MedicalKitCollector _medicalKitCollector;

        public DamageTargetTypes DamageTargetType => DamageTargetTypes.Player;

        public IHealth Health => _health;

        public BoxCollector BoxCollector => _boxCollector;

        public MedicalKitCollector MedicalKitCollector  => _medicalKitCollector;

        private void Awake()
        {
            _health = GetComponent<IHealth>();
            _boxCollector= GetComponent<BoxCollector>();
            _medicalKitCollector = GetComponent<MedicalKitCollector>();
        }
    }
}