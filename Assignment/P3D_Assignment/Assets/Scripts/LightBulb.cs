using UnityEngine;
using Random = UnityEngine.Random;

public class LightBulb : MonoBehaviour
{
    [SerializeField] private bool flickering;
    [SerializeField] private Vector2 flickerDelay = new Vector2(0.1f, 5f);
    [SerializeField] private Vector2 flickerOffTime = new Vector2(0.01f, 0.1f);

    private float _flickerDelayTimer;
    private Light[] _bulbs;

    private void Awake()
    {
        _bulbs = GetComponentsInChildren<Light>();
    }

    private void FixedUpdate()
    {
        if (flickering)
        {
            _flickerDelayTimer -= Time.deltaTime;
            if (_flickerDelayTimer <= 0)
            {
                SwitchLightsOff();
                _flickerDelayTimer = Random.Range(flickerDelay.x, flickerDelay.y);
                Invoke(nameof(SwitchLightsOn),Random.Range(flickerOffTime.x,flickerOffTime.y));
            }
        }
    }

    private void OnEnable()
    {
        SwitchLightsOn();
    }

    private void SwitchLightsOn()
    {
        foreach (Light bulb in _bulbs)
        {
            bulb.enabled = true;
        }
    }

    private void OnDisable()
    {
        SwitchLightsOff();
    }

    private void SwitchLightsOff()
    {
        foreach (Light bulb in _bulbs)
        {
            bulb.enabled = false;
        }
    }
}
