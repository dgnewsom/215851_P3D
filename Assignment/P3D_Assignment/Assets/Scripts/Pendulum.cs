using UnityEngine;

public class Pendulum : MonoBehaviour
{
    [SerializeField] private AudioClip tickSound;
    [SerializeField] private AudioClip tockSound;
    
    private AudioSource _audioSource;
    private bool _isTickNext;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pendulum"))
        {
            if (_isTickNext)
            {
                _audioSource.PlayOneShot(tickSound);
            }
            else
            {
                _audioSource.PlayOneShot(tockSound);
            }
            _isTickNext = !_isTickNext;
        }
    }
}
