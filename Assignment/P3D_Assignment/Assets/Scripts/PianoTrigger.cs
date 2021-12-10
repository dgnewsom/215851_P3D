using UnityEngine;

public class PianoTrigger : MonoBehaviour
{
    private AudioSource _audioSource;

    private bool _isPlaying;
    private void Start()
    {
        _audioSource = transform.parent.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_isPlaying){return;}
        if (other.CompareTag("Player"))
        {
            _audioSource.Play();
            _isPlaying = true;
        }
    }
}
