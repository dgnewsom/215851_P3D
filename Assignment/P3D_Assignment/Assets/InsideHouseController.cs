using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideHouseController : MonoBehaviour
{
    [SerializeField] private AudioSource[] outsideAudioSource;
    [SerializeField] private AudioSource[] insideAudioSource;

    private bool _isInside = false;
    
    private void Start()
    {
        SetInside(_isInside);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!_isInside)
            {
                _isInside = true;
                SetInside(_isInside);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isInside = false;
            SetInside(_isInside);
        }
    }

    public void SetInside(bool isInside)
    {
        if (isInside)
        {
            foreach (AudioSource audioSource in outsideAudioSource)
            {
                audioSource.volume = 0.2f;
            }

            foreach (AudioSource audioSource in insideAudioSource)
            {
                audioSource.volume = 1f;
            }
        }
        else
        {
            foreach (AudioSource audioSource in outsideAudioSource)
            {
                audioSource.volume = 1f;
            }

            foreach (AudioSource audioSource in insideAudioSource)
            {
                audioSource.volume = 0f;
            }
        }
    }
}
