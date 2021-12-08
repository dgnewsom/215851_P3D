using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TVTrigger : MonoBehaviour
{
    [SerializeField] private GameObject screen;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            screen.SetActive(true);
        }
    }
}
