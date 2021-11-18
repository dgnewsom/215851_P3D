using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private DoorState doorState;

    private Door door;
    private PlayerInputHandler inputHandler;

    // Start is called before the first frame update
    void Start()
    {
        door = GetComponentInParent<Door>();
        inputHandler = FindObjectOfType<PlayerInputHandler>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (inputHandler.Interact)
            {
                if (door.CurrentState.Equals(DoorState.Closed))
                {
                    door.SetDoorState(doorState);
                }
                else if (door.CurrentState.Equals(doorState))
                {
                    door.SetDoorState(DoorState.Closed);
                }
                else
                {
                    door.SetDoorState(DoorState.Closed);
                }
                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        door.SetDoorState(DoorState.Closed, 3f);
    }


}
