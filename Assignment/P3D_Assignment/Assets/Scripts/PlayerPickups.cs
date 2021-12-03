using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public enum PickupType
{
    Cuckoo,
    Vase,
    LoungeKey,
    KitchenKey
}

public class PlayerPickups : MonoBehaviour
{
    [SerializeField] private LayerMask pickupLayerMask;
    [SerializeField] private float raycastDistance = 5f;
    [SerializeField] private Transform cameraTransform;

    
    private Dictionary<PickupType,bool> _playerPickedUpItems = new Dictionary<PickupType, bool>();
    private PlayerInputHandler _playerInputHandler;
    private bool _inThrowCooldown;

    private void Start()
    {
        _playerInputHandler = FindObjectOfType<PlayerInputHandler>();
        foreach (PickupType pickupType in System.Enum.GetValues(typeof(PickupType)))
        {
            print(pickupType);
        }
    }

    private void FixedUpdate()
    {
        if (_playerInputHandler.Interact)
        {
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hitInfo, raycastDistance,
                pickupLayerMask, QueryTriggerInteraction.Ignore))
            {
                print($"{hitInfo.transform.gameObject.name}");
                if (hitInfo.transform.TryGetComponent<Pickup>(out Pickup pickup))
                {
                        _playerPickedUpItems.Add(pickup.Type,true);
                        Destroy(hitInfo.transform.gameObject);
                        UpdateItemDisplayed();
                }
            }
        }
        
        /*else if (_playerInputHandler.Throw)
        {
            if(_inThrowCooldown){return;}
            if (_playerPickedUpItems.Count > 0)
            {
                print($"Throw {_playerPickedUpItems[0]}");
                _inThrowCooldown = true;
                ThrowItem(_playerPickedUpItems[0]);
                Invoke(nameof(EndThrowCooldown),1f);
            }
        }*/
    }

    private void UpdateItemDisplayed()
    {
        foreach (Transform pickupObject in transform)
        {
            pickupObject.gameObject.SetActive(false);
        }

        if (_playerPickedUpItems.Count > 0)
        {
            /*foreach (var VARIABLE in COLLECTION)
            {
                
            }*/
        }
    }

    private void ThrowItem(PickupType playerPickedUpItem)
    {
        _playerPickedUpItems.Add(playerPickedUpItem,false);
    }

    private void EndThrowCooldown()
    {
        _inThrowCooldown = false;
    }
}
