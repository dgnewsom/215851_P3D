using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Wolves : MonoBehaviour
{
    [SerializeField] private AudioClip[] wolfSounds;
    [SerializeField] private Vector2 delayMinMax = new Vector2(10f, 20f);

    private DayNightController _dayNightController;
    private AudioSource _audioSource;
    private float _delayTimer;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _dayNightController = GetComponent<DayNightController>();
        ResetDelayTimer();
        PlayRandomWolfSound();
    }

    private void FixedUpdate()
    {
        if(_dayNightController.IsDaytime){return;}
        else
        {
            _delayTimer -= Time.deltaTime;
            if (_delayTimer <= 0f)
            {
                PlayRandomWolfSound();
                ResetDelayTimer();
            }
        }
    }

    private void ResetDelayTimer()
    {
        _delayTimer = Random.Range(delayMinMax.x, delayMinMax.y);
    }

    private void PlayRandomWolfSound()
    {
        int indexToPlay = Random.Range(1, wolfSounds.Length);
        AudioClip soundToPlay = wolfSounds[indexToPlay];
        _audioSource.PlayOneShot(soundToPlay);
        (wolfSounds[0], wolfSounds[indexToPlay]) = (wolfSounds[indexToPlay], wolfSounds[0]);
    }
    
}
