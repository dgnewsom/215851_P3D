using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private Transform keysPanel;
    [SerializeField] private GameObject keyDisplayPrefab;
    [SerializeField] private TMP_Text infoDisplay;
    [SerializeField] private GameObject startText;
    
    private void Start()
    {
        foreach (KeyType keyType in Enum.GetValues(typeof(KeyType)))
        {
            GameObject keyDisplay = Instantiate(keyDisplayPrefab, keysPanel);
            keyDisplay.GetComponent<KeyDisplay>().SetKeyType(keyType);
        }
        keysPanel.parent.gameObject.SetActive(false);
        Invoke(nameof(RemoveStartText),5f);
    }

    public void UpdateKeys(List<KeyType> keys)
    {
        foreach (Transform keyItem in keysPanel)
        {
            if (keyItem.TryGetComponent<KeyDisplay>(out KeyDisplay keyDisplay))
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

    public void SetInfoDisplay(string textToDisplay)
    {
        infoDisplay.text = textToDisplay;
    }

    private void RemoveStartText()
    {
        startText.SetActive(false);
        keysPanel.parent.gameObject.SetActive(true);
    }
}
