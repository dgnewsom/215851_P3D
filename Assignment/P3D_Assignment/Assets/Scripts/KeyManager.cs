using System.Collections.Generic;
using UnityEngine;

public enum KeyType
{
    BackDoor,
    Bathroom,
    Bedroom,
    Conservatory,
    FrontDoor,
    Kitchen,
    Lounge
}

public class KeyManager : MonoBehaviour
{
    private List<KeyType> keysFound = new List<KeyType>();
    private UIController _uiController;

    private void Start()
    {
        _uiController = FindObjectOfType<UIController>();
    }

    public void AddKey(KeyType keyToAdd)
    {
        if (!keysFound.Contains(keyToAdd))
        {
            keysFound.Add(keyToAdd);
            _uiController.UpdateKeys(keysFound);
        }
    }

    public bool CheckIfKeyHeld(KeyType keyToCheck)
    {
        foreach (KeyType key in keysFound)
        {
            if (key.Equals(keyToCheck))
            {
                return true;
            }
        }
        return false;
    }

    public static string GetKeyName(KeyType keyType)
    {
        switch (keyType)
        {
            case KeyType.BackDoor:
                return "Back Door";
            case KeyType.FrontDoor:
                return "Front Door";
            default:
                return keyType.ToString();
        }
    }
}
