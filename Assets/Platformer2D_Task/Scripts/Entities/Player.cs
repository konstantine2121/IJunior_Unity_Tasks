using UnityEngine;

namespace Platformer2D_Task
{
    public class Player : MonoBehaviour, IDamageTargetTypeContainer
    {
        public DamageTargetTypes DamageTargetType => DamageTargetTypes.Player;
    }
}