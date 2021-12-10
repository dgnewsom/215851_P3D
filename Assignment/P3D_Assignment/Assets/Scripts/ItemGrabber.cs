using System.Collections.Generic;
using UnityEngine;

public class ItemGrabber : MonoBehaviour
{
    [SerializeField] private LayerMask pickupLayerMask;
    [SerializeField] private float raycastDistance = 5f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] [Range(0f,20f)] private float throwForce = 10f;

    private readonly List<GameObject> _heldItems = new List<GameObject>();
    private KeyManager _keyManager;
    private PlayerInputHandler _playerInputHandler;
    private UIController _uiController;
    private GameObject _currentItem;
    private int _currentItemIndex;
    private bool _inCooldown;

    private void Start()
    {
        _playerInputHandler = FindObjectOfType<PlayerInputHandler>();
        _keyManager = FindObjectOfType<KeyManager>();
        _uiController = FindObjectOfType<UIController>();
    }

    private void Update()
    {
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hitInfo,
            raycastDistance,
            pickupLayerMask, QueryTriggerInteraction.Collide))
        {
            if (hitInfo.transform.TryGetComponent<Key>(out Key key))
            {
                _uiController.SetInfoDisplay($"Pick up {KeyManager.GetKeyName(key.DoorToOpen)} key");
            }
            else if (hitInfo.transform.TryGetComponent<Pickup>(out Pickup pickup))
            {
                _uiController.SetInfoDisplay($"Pick up {pickup.Type}");
            }
        }
        else
        {
            _uiController.SetInfoDisplay("");
        }

        if(_inCooldown){return;}
        
        if (_playerInputHandler.Throw)
        {
            if (hitInfo.transform)
            {
              
                if (hitInfo.transform.CompareTag("Key"))
                {
                    if (hitInfo.transform.TryGetComponent<Key>(out Key key))
                    {
                        _keyManager.AddKey(key.DoorToOpen);
                        Destroy(hitInfo.transform.gameObject);
                    }
                    _inCooldown = true;
                    Invoke(nameof(ResetCooldown),0.2f);
                    return;
                }

                if (!hitInfo.transform.CompareTag("Pickup") || _heldItems.Contains(hitInfo.transform.gameObject)) return;
                _currentItem = hitInfo.transform.gameObject;
                _heldItems.Add(_currentItem);
                _currentItemIndex = _heldItems.IndexOf(_currentItem);
                SetActiveObject();
                _currentItem.GetComponent<Rigidbody>().isKinematic = true;
                _currentItem.GetComponent<Collider>().enabled = false;
                Transform grabberTransform = transform;
                _currentItem.transform.parent = grabberTransform;
                _currentItem.transform.position = grabberTransform.position;
                _currentItem.transform.rotation = grabberTransform.rotation;
                _currentItem.transform.localScale = new Vector3(0.25f,0.25f,0.25f);
                _inCooldown = true;
                Invoke(nameof(ResetCooldown),0.2f);
            }
            else
            {
                if (!_currentItem) return;
                GameObject itemToThrow = _currentItem;
                _heldItems.Remove(itemToThrow);
                if (_heldItems.Count > 0)
                {
                    _currentItem = _heldItems[_heldItems.Count - 1];
                    _currentItemIndex = _heldItems.IndexOf(_currentItem);
                    SetActiveObject();
                }
                else
                {
                    _currentItem = null;
                }
                itemToThrow.transform.parent = null;
                itemToThrow.GetComponent<Collider>().enabled = true;
                itemToThrow.TryGetComponent<Rigidbody>(out Rigidbody itemRb);
                itemRb.isKinematic = false;
                itemRb.AddForce(cameraTransform.forward * (throwForce * 100f), ForceMode.Acceleration);
                itemToThrow.transform.localScale = new Vector3(1, 1, 1);
                _inCooldown = true;
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
        int numberOfItems = _heldItems.Count;
        if(numberOfItems <= 1){return;}
        if (_currentItemIndex == numberOfItems-1)
        {
            _currentItemIndex = 0;
        }
        else
        {
            _currentItemIndex++;
        }
        _currentItem = _heldItems[_currentItemIndex];
        SetActiveObject();
        _inCooldown = true;
        Invoke(nameof(ResetCooldown),0.2f);
    }

    private void PreviousItem()
    {
        int numberOfItems = _heldItems.Count;
        if(numberOfItems <= 1){return;}

        if (_currentItemIndex == 0)
        {
            _currentItemIndex = numberOfItems - 1;
        }
        else
        {
            _currentItemIndex--;
        }
        _currentItem = _heldItems[_currentItemIndex];
        SetActiveObject();
        _inCooldown = true;
        Invoke(nameof(ResetCooldown),0.2f);
    }

    private void SetActiveObject()
    {
        if (_heldItems.Count <= 0) return;

        for (int i = 0; i < _heldItems.Count; i++)
        {
            if (i == _currentItemIndex)
            {
                _heldItems[i].SetActive(true);
            }
            else
            {
                _heldItems[i].SetActive(false);
            }
        }
    }

    private void ResetCooldown()
    {
        _inCooldown = false;
    }
}
