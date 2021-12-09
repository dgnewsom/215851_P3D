using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndingTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent endingEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            endingEvent.Invoke();
        }
    }
}
