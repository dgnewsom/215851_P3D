using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorState
{
    OpenIn,
    Closed,
    OpenOut
}
public class Door : MonoBehaviour
{
    [SerializeField] private bool isLocked = false;
    [SerializeField] private DoorState currentState = DoorState.Closed;

    private Animator animator;
    private float coolDownDelay = 0.5f;
    private float cooldownTimer = 0;

    public DoorState CurrentState => currentState;


    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        if (cooldownTimer == 0f){return;}
        
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f)
        {
            cooldownTimer = 0f;
            SetDoorState(currentState);
        }
    }

    internal void SetDoorState(DoorState newState, float autoCloseDelay = 0f)
    {
        if(cooldownTimer > 0){return;}
        if (!isLocked && currentState != newState)
        {
            cooldownTimer = coolDownDelay;
            StartCoroutine(DoorStateDelay(autoCloseDelay,newState));
        }
    }

    private IEnumerator DoorStateDelay(float delay, DoorState newState)
    {
        yield return new WaitForSeconds(delay);
        currentState = newState;
        animator.SetInteger("OpenState",(int)currentState);
    }
}
