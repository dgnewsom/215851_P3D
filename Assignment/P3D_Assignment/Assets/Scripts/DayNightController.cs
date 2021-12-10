using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class DayNightController : MonoBehaviour
{
    [Header("Volume Profiles")]
    [SerializeField] private VolumeProfile dayProfile;
    [SerializeField] private VolumeProfile nightProfile;
    [Header("Audio")]
    [SerializeField] private AudioClip nightAmbientSound;
    [SerializeField] private AudioClip dayAmbientSound;
    [Header("Event to trigger on switch to daytime")]
    [SerializeField] private UnityEvent endingEvent;

    private LightBulb[] _lights;
    private bool _isDaytime;
    private AudioSource _outsideAudioSource;
    private Volume _volume;
    private Animator _animator;
    private float _animationSpeed;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Daytime = Animator.StringToHash("IsDaytime");

    public bool IsDaytime => _isDaytime;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _volume = GetComponentInChildren<Volume>();
        _lights = FindObjectsOfType<LightBulb>();
        _animationSpeed = _animator.GetFloat(Speed);
        _outsideAudioSource = GetComponent<AudioSource>();
        SetNighttime();
    }

    private void UpdateAnimator()
    {
        _animator.SetBool(Daytime,_isDaytime);
    }

    private void SetDaytime()
    {
        _isDaytime = true;
        _outsideAudioSource.clip = dayAmbientSound;
        _outsideAudioSource.Play();
        _volume.profile = dayProfile;
        UpdateAnimator();
        Invoke(nameof(EndingEvent),1f);
    }

    private void SetNighttime()
    {
        _isDaytime = false;
        _outsideAudioSource.clip = nightAmbientSound;
        _outsideAudioSource.Play();
        _volume.profile = nightProfile;
        UpdateAnimator();
    }

    [ContextMenu("Toggle Day/Night")]
    public void ToggleDayNight()
    {
        _isDaytime = !_isDaytime;
        if (_isDaytime)
        {
            SetDaytime();
        }
        else
        {
            SetNighttime();
        }
    }

    [ContextMenu("Skip To Day")]
    public void SkipToDaytime()
    {
        SetDaytime();
        _animator.SetFloat(Speed,5f);
    }

    [ContextMenu("Skip To Night")]
    public void SkipToNighttime()
    {
        SetNighttime();
        _animator.SetFloat(Speed,5f);
    }

    public void EnableHouseLights()
    {
        foreach (LightBulb lightBulb in _lights)
        {
            lightBulb.enabled = true;
        }
    }
    
    public void DisableHouseLights()
    {
        foreach (LightBulb lightBulb in _lights)
        {
            lightBulb.enabled = false;
        }
        
    }

    public void ResetSpeed()
    {
        _animator.SetFloat(Speed,_animationSpeed);
    }

    private void EndingEvent()
    {
        endingEvent.Invoke();
    }
}
