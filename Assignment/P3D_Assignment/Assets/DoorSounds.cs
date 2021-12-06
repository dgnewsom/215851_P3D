using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSounds : MonoBehaviour
{
    [SerializeField] private AudioClip doorCloseSound;
    [SerializeField] private AudioClip doorOpenSound;
    [SerializeField] private AudioClip doorLockSound;
    [SerializeField] private AudioClip doorUnlockSound;

    private Door _door;

    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _door = transform.parent.GetComponent<Door>();
    }

    public void PlayDoorCloseSound()
    {
        _audioSource.PlayOneShot(doorCloseSound);
        if (_door.IsLocked)
        {
            Invoke(nameof(PlayDoorLockSound),0.25f);
        }
    }    
    
    public void PlayDoorOpenSound()
    {
        _audioSource.PlayOneShot(doorOpenSound);
    }    
    
    public void PlayDoorLockSound()
    {
        _audioSource.PlayOneShot(doorLockSound);
    }    
    
    public void PlayDoorUnlockSound()
    {
        _audioSource.PlayOneShot(doorUnlockSound);
    }
}
