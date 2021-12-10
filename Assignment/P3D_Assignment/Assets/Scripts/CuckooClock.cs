using UnityEngine;

public class CuckooClock : MonoBehaviour
{
    [SerializeField] private AudioClip cuckooSound;
    [SerializeField] private GameObject cuckooToDrop;
    
    private AudioSource _cuckooClockAudioSource;
    private bool _hasPlayed;
    private static readonly int Cuckoo = Animator.StringToHash("Cuckoo");

    private void Start()
    {
        _cuckooClockAudioSource = transform.parent.GetComponent<AudioSource>();
        cuckooToDrop.SetActive(false);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(_hasPlayed){return;}
        
        if (other.CompareTag("Player"))
        {
            GetComponent<Animator>().SetBool(Cuckoo,true);
            _hasPlayed = true;
        }
    }

    public void PlayCuckooSound()
    {
        _cuckooClockAudioSource.PlayOneShot(cuckooSound);
    }

    public void DropCuckoo()
    {
        cuckooToDrop.SetActive(true);
    }
}
