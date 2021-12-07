using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    Cuckoo,
    Vase,
    FruitBowl
}

public class Pickup : MonoBehaviour
{
    [SerializeField] private PickupType _pickupType;

    public PickupType Type => _pickupType;

    public static string GetPickupName(PickupType pickupType)
    {
        switch (pickupType)
        {
            case PickupType.FruitBowl:
                return "Fruit Bowl";
            default:
                return pickupType.ToString();
        }
    }
}
