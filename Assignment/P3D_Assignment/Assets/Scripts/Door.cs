using System.Collections;
using UnityEngine;

//Door states (indexes match animator int values)
public enum DoorState
{
    OpenIn,
    Closed,
    OpenOut
}
public class Door : MonoBehaviour
{
    [Header("Lock settings")]
    [SerializeField] private bool isLocked;
    [Header("Trigger daytime / ending")]
    [SerializeField] private bool triggerDaytime;
    [Header("KeyType required to unlock")]
    [SerializeField] private KeyType keyType;

    private DoorState _currentState = DoorState.Closed;
    private Animator _animator;
    private readonly float coolDownDelay = 0.5f;
    private float _cooldownTimer;
    private KeyManager _keyManager;
    private DoorTexts _doorTexts;
    private DoorSounds _doorSounds;
    private static readonly int OpenState = Animator.StringToHash("OpenState");

    public DoorState CurrentState => _currentState;

    public bool IsLocked => isLocked;

    public KeyType Type => keyType;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _keyManager = FindObjectOfType<KeyManager>();
        _doorTexts = GetComponentInChildren<DoorTexts>();
        _doorSounds = GetComponentInChildren<DoorSounds>();
    }

    private void FixedUpdate()
    {
        if (_cooldownTimer == 0f){return;}
        
        _cooldownTimer -= Time.deltaTime;

        if (_cooldownTimer <= 0f)
        {
            _cooldownTimer = 0f;
            SetDoorState(_currentState);
        }
    }

    internal void SetDoorState(DoorState newState, float autoCloseDelay = 0f)
    {
        if(_cooldownTimer > 0){return;}

        if (isLocked)
        {
            if (_keyManager.CheckIfKeyHeld(keyType) && newState != DoorState.Closed)
            {
                isLocked = false;
                _doorSounds.PlayDoorUnlockSound();
                _doorTexts.SetDoorText();
            }
        }
        if (!isLocked && _currentState != newState)
        {
            _doorTexts.ClearDoorText();
            _cooldownTimer = coolDownDelay;
            StartCoroutine(DoorStateDelay(autoCloseDelay,newState));
            if (triggerDaytime)
            {
                DayNightController dayNightController = FindObjectOfType<DayNightController>();
                if (!dayNightController.IsDaytime)
                {
                    dayNightController.SkipToDaytime();
                }
            }
        }
    }

    private IEnumerator DoorStateDelay(float delay, DoorState newState)
    {
        yield return new WaitForSeconds(delay);
        _currentState = newState;
        _animator.SetInteger(OpenState,(int)_currentState);
    }

    public void LockDoor()
    {
        if (isLocked) return;
        isLocked = true;
        StartCoroutine(DoorStateDelay(0,DoorState.Closed));
    }
}
