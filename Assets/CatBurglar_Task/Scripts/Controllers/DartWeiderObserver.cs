using UnityEngine;

namespace CatBurglar_Task
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class DartWeiderObserver : MonoBehaviour
    {        
        [SerializeField] private AlarmPlayer _loudSpeaker;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (CheckDartWeider(collision))
            {
                _loudSpeaker?.Play();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (CheckDartWeider(collision))
            {
                _loudSpeaker?.Stop();
            }
        }

        private bool CheckDartWeider(Collider2D collision)
        {
            if (collision == null)
            {
                return false;
            }

            return collision.gameObject.TryGetComponent(out DartWeider dartWeider);
        }
    }

}