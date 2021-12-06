using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorTexts : MonoBehaviour
{
    [SerializeField] private GameObject[] textPanels;
    
    private Door _door;

    private void Start()
    {
        _door = GetComponentInParent<Door>();
        ClearDoorText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetDoorText(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ClearDoorText();
    }
    
    public void SetDoorText(Transform playerTransform)
    {
        ClearDoorText();
        GameObject nearestTextPanel = null;
        float distance = float.MaxValue;
        foreach (GameObject textPanel in textPanels)
        {
            float doorTextDistance = Vector3.Distance(textPanel.transform.position, playerTransform.position);
            if (doorTextDistance < distance)
            {
                distance = doorTextDistance;
                nearestTextPanel = textPanel;
            }
        }
        if(nearestTextPanel == null){return;}
        nearestTextPanel.SetActive(true);
        if (_door.IsLocked)
        {
            UpdateDoorText(nearestTextPanel.GetComponentInChildren<TMP_Text>(),$"{KeyManager.GetKeyName(_door.Type)}\nkey\nRequired");
        }
        else
        {
            UpdateDoorText(nearestTextPanel.GetComponentInChildren<TMP_Text>(), "LMB to open");
        }
    }

    public void ClearDoorText()
    {
        foreach (GameObject textPanel in textPanels)
        {
            textPanel.SetActive(false);
        }
    }
    
    private void UpdateDoorText(TMP_Text nearestDoorText, string newDoorText)
    {
        nearestDoorText.text = newDoorText;
    }
}
