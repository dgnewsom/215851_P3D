using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServingHatchTrigger : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioSource;
    private bool isPlaying;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = transform.parent.GetComponentInChildren<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isPlaying){return;}

        if (other.CompareTag("Player"))
        {
            isPlaying = true;
            _audioSource.Play();
            _animator.SetBool("isActive",true);
        }
    }
}
