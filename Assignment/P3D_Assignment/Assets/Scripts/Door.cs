using System.Collections;
using TMPro;
using UnityEngine;

public enum DoorState
{
    OpenIn,
    Closed,
    OpenOut
}
public class Door : MonoBehaviour
{
    [SerializeField] private bool isLocked;
    [SerializeField] private bool lockOnEntry;
    [SerializeField] private bool triggerDaytime;
    [SerializeField] private KeyType keyType;

    private DoorState _currentState = DoorState.Closed;
    private Animator _animator;
    private readonly float coolDownDelay = 0.5f;
    private float _cooldownTimer;
    private static readonly int OpenState = Animator.StringToHash("OpenState");
    private KeyManager _keyManager;
    private DoorTexts _doorTexts;
    

    public DoorState CurrentState => _currentState;

    public bool IsLocked => isLocked;

    public KeyType Type => keyType;


    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _keyManager = FindObjectOfType<KeyManager>();
        _doorTexts = GetComponentInChildren<DoorTexts>();
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
            if (_keyManager.CheckIfKeyHeld(keyType))
            {
                isLocked = false;
                print("implement lock sounds!");
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
        if (lockOnEntry == true && FindObjectOfType<InsideHouseController>().IsInside && newState.Equals(DoorState.Closed))
        {
            isLocked = true;
        }
    }
}
