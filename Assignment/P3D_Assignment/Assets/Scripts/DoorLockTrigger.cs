using UnityEngine;

public class DoorLockTrigger : MonoBehaviour
{
    private Door _door;

    private void Start()
    {
        _door = transform.parent.GetComponent<Door>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _door.LockDoor();
        }
    }
}
