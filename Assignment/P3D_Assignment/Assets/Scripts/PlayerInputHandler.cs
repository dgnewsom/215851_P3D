using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif


	public class PlayerInputHandler : MonoBehaviour
	{
        [Header("Movement Settings")]
        [SerializeField] private bool analogMovement;

        [Header("Mouse Cursor Settings")]
        [SerializeField] private bool LockCursor = true;

        private PlayerInput input;

        //Input Variables
        private Vector2 move;
        private Vector2 look;
        private bool jump;
        private bool sprint;
        private bool interact;
        private bool grab;

        //Public getters and setters
        public Vector2 Move => move;
        public Vector2 Look => look;

        public bool Jump
        {
            get => jump;
            set => jump = value;
        }

        public bool Sprint => sprint;

        public bool Interact => interact;

        public bool Grab => grab;

        public bool AnalogMovement => analogMovement;

        private void Start()
        {
            input = GetComponent<PlayerInput>();
            if (LockCursor)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        public void OnControlsChanged()
        {
            print(input.currentControlScheme);
        }

		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			LookInput(value.Get<Vector2>());
            print(value.Get<Vector2>());
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

        public void OnGrab(InputValue value)
        {
            GrabInput(value.isPressed);
        }

        private void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

        private void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

        private void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

        private void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
        
        private void InteractInput(bool newInteractState)
        {
            interact = newInteractState;
        }

        private void GrabInput(bool newGrabState)
        {
            grab = newGrabState;
        }
    }
	
