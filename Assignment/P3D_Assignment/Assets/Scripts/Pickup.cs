using UnityEngine;

public enum PickupType
{
    Cuckoo,
    Vase,
    FruitBowl
}

public class Pickup : MonoBehaviour
{
    [SerializeField] private PickupType pickupType;

    public PickupType Type => pickupType;

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
