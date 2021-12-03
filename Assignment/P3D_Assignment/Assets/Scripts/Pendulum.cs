using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    [SerializeField] private AudioClip tickSound;
    [SerializeField] private AudioClip tockSound;
    
    private AudioSource _audioSource;
    private bool isTickNext;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pendulum"))
        {
            if (isTickNext)
            {
                _audioSource.PlayOneShot(tickSound);
            }
            else
            {
                _audioSource.PlayOneShot(tockSound);
            }
            isTickNext = !isTickNext;
        }
    }
}
