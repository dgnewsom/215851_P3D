using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    Cuckoo,
    Vase
}

public class Pickup : MonoBehaviour
{
    [SerializeField] private PickupType _pickupType;

    public PickupType Type => _pickupType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
