using UnityEngine;

namespace SpawnEnemies_Task
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class TrooperDestroyer : MonoBehaviour
    {
        private const float DestroyDelay = 0.5f;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Trooper trooper))
            {
                Destroy(collision.gameObject, DestroyDelay);
            }
        }
    }
}