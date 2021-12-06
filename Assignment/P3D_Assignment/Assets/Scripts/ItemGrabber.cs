using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrabber : MonoBehaviour
{
    [SerializeField] private LayerMask pickupLayerMask;
    [SerializeField] private float raycastDistance = 5f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] [Range(0f,20f)] private float throwForce = 10f;
    
    private KeyManager _keyManager;
    private PlayerInputHandler _playerInputHandler;
    private List<GameObject> heldItems = new List<GameObject>();
    private GameObject currentItem;
    private int currentItemIndex = 0;
    private bool inCooldown = false;

    // Start is called before the first frame update
    void Start()
    {
        _playerInputHandler = FindObjectOfType<PlayerInputHandler>();
        _keyManager = FindObjectOfType<KeyManager>();
    }

    private void Update()
    {
        if(inCooldown){return;}
        if (_playerInputHandler.Throw)
        {
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hitInfo,
                raycastDistance,
                pickupLayerMask, QueryTriggerInteraction.Collide))
            {
                if (hitInfo.transform.CompareTag("Key"))
                {
                    if (hitInfo.transform.TryGetComponent<Key>(out Key key))
                    {
                        _keyManager.AddKey(key.DoorToOpen);
                        Destroy(hitInfo.transform.gameObject);
                    }
                    inCooldown = true;
                    Invoke(nameof(ResetCooldown),0.2f);
                    return;
                }

                if (hitInfo.transform.CompareTag("Pickup") && !heldItems.Contains(hitInfo.transform.gameObject))
                {
                    currentItem = hitInfo.transform.gameObject;
                    heldItems.Add(currentItem);
                    currentItemIndex = heldItems.IndexOf(currentItem);
                    SetActiveObject();
                    currentItem.GetComponent<Rigidbody>().isKinematic = true;
                    currentItem.GetComponent<Collider>().enabled = false;
                    currentItem.transform.parent = transform;
                    currentItem.transform.position = transform.position;
                    currentItem.transform.rotation = transform.rotation;
                    inCooldown = true;
                    Invoke(nameof(ResetCooldown),0.2f);
                    return;
                }
            }
            else
            {
                if (!currentItem) return;
                GameObject itemToThrow = currentItem;
                print($"Throw {itemToThrow.name}");
                heldItems.Remove(itemToThrow);
                if (heldItems.Count > 0)
                {
                    currentItem = heldItems[heldItems.Count - 1];
                    SetActiveObject();
                }
                else
                {
                    currentItem = null;
                }
                itemToThrow.transform.parent = null;
                itemToThrow.GetComponent<Collider>().enabled = true;
                itemToThrow.TryGetComponent<Rigidbody>(out Rigidbody itemRb);
                itemRb.isKinematic = false;
                itemRb.AddForce(cameraTransform.forward * throwForce * 100f, ForceMode.Acceleration);
                inCooldown = true;
                Invoke(nameof(ResetCooldown),0.2f);
            }
        }
        else
        {
            if (_playerInputHandler.NextItem)
            {
                NextItem();
            }
            else if (_playerInputHandler.PreviousItem)
            {
                PreviousItem();
            }
        }
    }

    private void NextItem()
    {
        print("Next");
        int numberOfItems = heldItems.Count;
        if(numberOfItems <= 1){return;}
        if (currentItemIndex == numberOfItems-1)
        {
            currentItemIndex = 0;
        }
        else
        {
            currentItemIndex++;
        }
        currentItem = heldItems[currentItemIndex];
        SetActiveObject();
        inCooldown = true;
        Invoke(nameof(ResetCooldown),0.2f);
    }

    private void PreviousItem()
    {
        print("Previous");
        int numberOfItems = heldItems.Count;
        if(numberOfItems <= 1){return;}

        if (currentItemIndex == 0)
        {
            currentItemIndex = numberOfItems - 1;
        }
        else
        {
            currentItemIndex--;
        }
        currentItem = heldItems[currentItemIndex];
        SetActiveObject();
        inCooldown = true;
        Invoke(nameof(ResetCooldown),0.2f);
    }

    private void SetActiveObject()
    {
        if (heldItems.Count <= 0) return;

        for (int i = 0; i < heldItems.Count; i++)
        {
            if (i == currentItemIndex)
            {
                heldItems[i].SetActive(true);
            }
            else
            {
                heldItems[i].SetActive(false);
            }
        }
    }

    private void ResetCooldown()
    {
        inCooldown = false;
    }
}
