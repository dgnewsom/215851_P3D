using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoorTexts : MonoBehaviour
{
    [SerializeField] private GameObject[] textPanels;
    
    private Door _door;
    private KeyManager _keyManager;
    private Transform _player;
    private Animator _animator;

    private void Start()
    {
        _door = GetComponentInParent<Door>();
        _keyManager = FindObjectOfType<KeyManager>();
        _player = GameObject.FindWithTag("Player").transform;
        _animator = transform.parent.GetComponentInChildren<Animator>();
        ClearDoorText();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetDoorText();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Invoke(nameof(ClearDoorText),1f);
    }
    
    public void SetDoorText()
    {
        ClearDoorText();
        GameObject nearestTextPanel = null;
        float distance = float.MaxValue;
        foreach (GameObject textPanel in textPanels)
        {
            float doorTextDistance = Vector3.Distance(textPanel.transform.position, _player.position);
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
            if (_keyManager.CheckIfKeyHeld(_door.Type))
            {
                UpdateDoorText(nearestTextPanel.GetComponentInChildren<TMP_Text>(),$"LMB to \nUnlock");
            }
            else
            {
                UpdateDoorText(nearestTextPanel.GetComponentInChildren<TMP_Text>(),$"{KeyManager.GetKeyName(_door.Type)}\nkey\nRequired");
            }
        }
        else
        {
            if(_animator.GetInteger("OpenState") == 1)
            {
                UpdateDoorText(nearestTextPanel.GetComponentInChildren<TMP_Text>(),$"LMB to \nOpen");
            }
            else
            {
                ClearDoorText();
            }
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
