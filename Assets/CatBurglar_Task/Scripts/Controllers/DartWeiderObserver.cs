using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DartWeiderObserver : MonoBehaviour
{
    private const string DartWeiderName = CharactersNames.DartWeider;

    [SerializeField] private ImperialMarchPlayer _loudSpeaker;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(CheckDartWeider(collision))
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

        return NameContainer.NameEquals(collision.gameObject, DartWeiderName);
    }
}
