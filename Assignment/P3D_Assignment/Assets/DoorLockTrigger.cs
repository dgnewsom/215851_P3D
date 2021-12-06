using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLockTrigger : MonoBehaviour
{
    [SerializeField] private Door _door;

    private void Start()
    {
        _door = transform.parent.GetComponent<Door>();
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _door.LockDoor();
        }
    }
}
