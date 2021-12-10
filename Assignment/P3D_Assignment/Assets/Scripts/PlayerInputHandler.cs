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

    private Vector2 _move;
    private Vector2 _look;
    private bool _jump;
    private bool _sprint;
    private bool _interact;
    private bool _throw;
    private bool _nextItem;
    private bool _previousItem;

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

    public bool NextItem => _nextItem;

    public bool PreviousItem => _previousItem;

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
		_move = value.Get<Vector2>();
	}

	public void OnLook(InputValue value)
	{
		_look = value.Get<Vector2>();
	}

	public void OnJump(InputValue value)
	{
		_jump = value.isPressed;
	}

	public void OnSprint(InputValue value)
	{
		_sprint = value.isPressed;
	}

    public void OnInteract(InputValue value)
    {
	    _interact = value.isPressed;
    }

    public void OnThrow(InputValue value)
    {
	    _throw = value.isPressed;
    }
    
    public void OnNextItem(InputValue value)
    {
	    _nextItem = value.isPressed;
    }
    
    public void OnPreviousItem(InputValue value)
    {
	    _previousItem = value.isPressed;
    }
}
	
