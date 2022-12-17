using UnityEngine;

namespace Platformer2D_Task
{
    public static class GameObjectExtensions
    {
        public static DamageTargetType GetDamageTargetType(this GameObject gameObject)
        {
            if (gameObject.TryGetComponent(out IDamageTargetTypeContainer container))
            {
                return container.DamageTargetType;
            }

            return DamageTargetType.None;
        }
    }
}
