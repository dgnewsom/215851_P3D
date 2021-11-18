using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif


	public class PlayerInput : MonoBehaviour
	{
		[Header("Character Input Values")]
		[SerializeField] private Vector2 move;
        [SerializeField] private Vector2 look;
        [SerializeField] private bool jump;
        [SerializeField] private bool sprint;
        [SerializeField] private bool interact;
        [SerializeField] private bool grab;

		[Header("Movement Settings")]
        [SerializeField] private bool analogMovement;

#if !UNITY_IOS || !UNITY_ANDROID
		[Header("Mouse Cursor Settings")]
        [SerializeField] private bool cursorLocked = true;
        [SerializeField] private bool cursorInputForLook = true;

		//Public getters
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
#endif

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
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
#else
	// old input sys if we do decide to have it (most likely wont)...
#endif


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

#if !UNITY_IOS || !UNITY_ANDROID

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

#endif

	}
	
