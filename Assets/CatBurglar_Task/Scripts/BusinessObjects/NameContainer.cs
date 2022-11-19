using UnityEngine;

public class NameContainer : MonoBehaviour
{
    public string Name = string.Empty;

    public static bool TryGetName(GameObject gameObject, out string name)
    {
        name = string.Empty;

        if (gameObject == null)
        {
            return false;
        }

        var nameContainer = gameObject.GetComponent<NameContainer>();

        if (nameContainer == null)
        {
            return false;
        }
        else
        {
            name = nameContainer.Name;
            return true;
        }
    }

    public static bool NameEquals(GameObject gameObject, string name)
    {
        if (TryGetName(gameObject, out string objectName))
        {
            return string.Equals(name, objectName);
        }

        return false;
    }
}
