using UnityEngine;

namespace Platformer2D_Task
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Box : BaseCollectable
    {
        public override CollectableTypes CollectableType => CollectableTypes.Box;

        protected override void PerformAfterTake()
        {
            Destroy(gameObject);
        }
    }
}