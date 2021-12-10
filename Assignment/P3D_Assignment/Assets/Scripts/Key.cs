using UnityEngine;


public class Key : MonoBehaviour
{
    [SerializeField] private KeyType doorToOpen;

    public KeyType DoorToOpen => doorToOpen;
}
