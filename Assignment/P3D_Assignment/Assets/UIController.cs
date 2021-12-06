using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private Transform keysPanel;
    [SerializeField] private GameObject keyDisplayPrefab;
    
    private void Start()
    {
        foreach (KeyType keyType in Enum.GetValues(typeof(KeyType)))
        {
            GameObject keyDisplay = Instantiate(keyDisplayPrefab, keysPanel);
            keyDisplay.GetComponent<KeyDisplay>().SetKeyType(keyType);
        }
    }

    public void UpdateKeys(List<KeyType> keys)
    {
            foreach (Transform keyitem in keysPanel)
            {
                if (keyitem.TryGetComponent<KeyDisplay>(out KeyDisplay keyDisplay))
                {
                    if (keys.Contains(keyDisplay.Type))
                    {
                        keyDisplay.SetCollected(true);
                    }
                    else
                    {
                        keyDisplay.SetCollected(false);
                    }
                }
            } 
    }
}
