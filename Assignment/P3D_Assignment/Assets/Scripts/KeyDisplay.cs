using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text keyDisplayText;
    [SerializeField] private Image crossImage;

    private KeyType _keyType;

    public KeyType Type => _keyType;

    public void SetKeyType(KeyType keyType)
    {
        _keyType = keyType;
        keyDisplayText.text = $"{KeyManager.GetKeyName(keyType)}";
    }

    public void SetCollected(bool collected)
    {
        crossImage.enabled = !collected;
    }
}
