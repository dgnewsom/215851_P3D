using System;using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Key : MonoBehaviour
{
    [SerializeField] private KeyType doorToOpen;

    public KeyType DoorToOpen => doorToOpen;
    
}
