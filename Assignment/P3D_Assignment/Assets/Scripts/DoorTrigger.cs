using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private DoorState doorState;

    private Door _door;
    private PlayerInputHandler _inputHandler;

    // Start is called before the first frame update
    void Start()
    {
        _door = GetComponentInParent<Door>();
        _inputHandler = FindObjectOfType<PlayerInputHandler>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_inputHandler.Interact)
            {
                if (_door.CurrentState.Equals(DoorState.Closed))
                {
                    _door.SetDoorState(doorState);
                }
                else if (_door.CurrentState.Equals(doorState))
                {
                    _door.SetDoorState(DoorState.Closed);
                }
                else
                {
                    _door.SetDoorState(DoorState.Closed);
                }
                
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _door.SetDoorState(DoorState.Closed, 3f);
    }


}
