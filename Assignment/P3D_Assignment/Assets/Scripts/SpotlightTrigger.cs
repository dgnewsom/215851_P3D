using UnityEngine;

public class SpotlightTrigger : MonoBehaviour
{
    [SerializeField] private float lightTimeout = 5f;

    private LightBulb _light;
    private bool _playerIsInTrigger;
    private float _lightTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        _light = transform.parent.GetComponentInChildren<LightBulb>();
        _light.enabled = false;
        _lightTimer = lightTimeout;
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerIsInTrigger)
        {
            _lightTimer = lightTimeout;
        }
        else
        {
            _lightTimer -= Time.deltaTime;
        }

        if (_lightTimer < 0f)
        {
            _light.enabled = false;
            _lightTimer = lightTimeout;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIsInTrigger = true;
            _light.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIsInTrigger = false;
        }
    }
}
