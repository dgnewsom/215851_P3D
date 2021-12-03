using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTexts : MonoBehaviour
{
    private Door _door;
    private void Start()
    {
        _door = GetComponentInParent<Door>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _door.SetDoorText();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _door.ClearDoorText();
    }
}
