using UnityEngine;

namespace Platformer2D_Task
{
    public class Player : MonoBehaviour, IDamageTargetTypeContainer
    {
        public DamageTargetType DamageTargetType => DamageTargetType.Player;
    }
}