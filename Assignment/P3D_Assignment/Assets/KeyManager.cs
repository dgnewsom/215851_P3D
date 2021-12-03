using System;
using System.Collections;
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
    private List<Key> keysFound = new List<Key>();
    private UIController _uiController;

    public void AddKey(Key keyToAdd)
    {
        keysFound.Add(keyToAdd);
        _uiController.UpdateKeys();
    }

    public bool CheckIfKeyHeld(KeyType keyToCheck)
    {
        foreach (Key key in keysFound)
        {
            if (key.DoorToOpen.Equals(keyToCheck))
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
                break;
            case KeyType.FrontDoor:
                return "Front Door";
                break;
            default:
                return keyType.ToString();
        }
    }
}
