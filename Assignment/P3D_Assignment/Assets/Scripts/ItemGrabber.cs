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
    private PlayerInputHandler _playerInputHandler;
    private GameObject currentItem;

    // Start is called before the first frame update
    void Start()
    {
        _playerInputHandler = FindObjectOfType<PlayerInputHandler>();
    }

    private void Update()
    {
        if (_playerInputHandler.Throw)
        {
            if (!currentItem)
            {
                if (!Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hitInfo,
                    raycastDistance,
                    pickupLayerMask, QueryTriggerInteraction.Collide)) return;
                if (hitInfo.transform.CompareTag("Pickup"))
                {
                    currentItem = hitInfo.transform.gameObject;
                    currentItem.GetComponent<Rigidbody>().isKinematic = true;
                    currentItem.GetComponent<Collider>().enabled = false;
                }
                else if (hitInfo.transform.CompareTag("Key"))
                {
                    
                }
            }
            else
            {
                currentItem.transform.position = transform.position;
                currentItem.transform.rotation = Quaternion.Lerp(currentItem.transform.rotation, transform.rotation, Time.deltaTime * 100f);
            }
        }
        else
        {
            if (!currentItem) return;
            currentItem.GetComponent<Collider>().enabled = true;
            currentItem.TryGetComponent<Rigidbody>(out Rigidbody itemRb);
            itemRb.isKinematic = false;
            itemRb.AddForce(cameraTransform.forward * throwForce * 100f, ForceMode.Acceleration);
            currentItem = null;
        }
    }
}
