using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private bool analogMovement;

    [Header("Mouse Cursor Settings")]
    [SerializeField] private bool lockCursor;

    private PlayerInput _input;

    //Input Variables
    private Vector2 _move;
    private Vector2 _look;
    private bool _jump;
    private bool _sprint;
    private bool _interact;
    private bool _throw;

    //Public getters and setters
    public Vector2 Move => _move;
    public Vector2 Look => _look;

    public bool Jump
    {
        get => _jump;
        set => _jump = value;
    }

    public bool Sprint => _sprint;

    public bool Interact => _interact;

    public bool Throw => _throw;

    public bool AnalogMovement => analogMovement;

    private void Start()
    {
        _input = GetComponent<PlayerInput>();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void OnControlsChanged()
    {
        print(_input.currentControlScheme);
    }

	public void OnMove(InputValue value)
	{
		MoveInput(value.Get<Vector2>());
	}

	public void OnLook(InputValue value)
	{
		LookInput(value.Get<Vector2>());
	}

	public void OnJump(InputValue value)
	{
		JumpInput(value.isPressed);
	}

	public void OnSprint(InputValue value)
	{
		SprintInput(value.isPressed);
	}

    public void OnInteract(InputValue value)
    {
		InteractInput(value.isPressed);
    }

    public void OnThrow(InputValue value)
    {
        ThrowInput(value.isPressed);
    }

    private void MoveInput(Vector2 newMoveDirection)
	{
		_move = newMoveDirection;
	} 

    private void LookInput(Vector2 newLookDirection)
	{
		_look = newLookDirection;
	}

    private void JumpInput(bool newJumpState)
	{
		_jump = newJumpState;
	}

    private void SprintInput(bool newSprintState)
	{
		_sprint = newSprintState;
	}
    
    private void InteractInput(bool newInteractState)
    {
        _interact = newInteractState;
    }

    private void ThrowInput(bool newThrowState)
    {
        _throw = newThrowState;
    }
}
	
