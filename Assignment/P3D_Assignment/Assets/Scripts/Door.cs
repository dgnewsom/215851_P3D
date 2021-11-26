using System.Collections;
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
    [SerializeField] private bool triggerDaytime;

    private DoorState _currentState = DoorState.Closed;
    private Animator _animator;
    private readonly float coolDownDelay = 0.5f;
    private float _cooldownTimer;
    private static readonly int OpenState = Animator.StringToHash("OpenState");

    public DoorState CurrentState => _currentState;


    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
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
        if (!isLocked && _currentState != newState)
        {
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
}
