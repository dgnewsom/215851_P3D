using UnityEngine;

public class ServingHatchTrigger : MonoBehaviour
{
    private Animator _animator;
    private AudioSource _audioSource;
    private bool _isPlaying;
    private static readonly int IsActive = Animator.StringToHash("isActive");

    // Start is called before the first frame update
    void Start()
    {
        _animator = transform.parent.GetComponentInChildren<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_isPlaying){return;}

        if (other.CompareTag("Player"))
        {
            _isPlaying = true;
            _audioSource.Play();
            _animator.SetBool(IsActive,true);
        }
    }
}
