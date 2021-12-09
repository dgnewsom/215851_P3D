using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoTrigger : MonoBehaviour
{
    private AudioSource _audioSource;

    private bool isPlaying = false;
    private void Start()
    {
        _audioSource = transform.parent.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isPlaying){return;}
        if (other.CompareTag("Player"))
        {
            _audioSource.Play();
            isPlaying = true;
        }
    }
}
