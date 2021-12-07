using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class FireBasket : MonoBehaviour
{
    [SerializeField] private VisualEffect fireEffect;
    [SerializeField] private PickupType triggerObject;
    [SerializeField] private UnityEvent triggerEvent;

    private Light _fireGlow;
    private Vector2 _fireGlowIntensityRange = new Vector2(40,80);

    private void Start()
    {
        _fireGlow = GetComponentInChildren<Light>();
    }

    private void FixedUpdate()
    {
        _fireGlow.intensity = Mathf.Lerp(_fireGlow.intensity, Random.Range(_fireGlowIntensityRange.x, _fireGlowIntensityRange.y), Time.deltaTime * 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent<Pickup>(out Pickup pickup)) return;
        if (pickup.Type.Equals(triggerObject))
        {
            TriggerAction();
            Destroy(other.gameObject);
        }
    }

    private void TriggerAction()
    {
        _fireGlowIntensityRange *= 2;
        fireEffect.SetFloat("SpawnRate",500f);
        fireEffect.SetVector3("MinVelocity",new Vector3(0.5f,1.5f,0.25f));
        fireEffect.SetVector3("MaxVelocity",new Vector3(0.5f,2f,0.25f));
        fireEffect.SetFloat("BaseSize",0.75f);
        Invoke(nameof(EnableItem),0.5f);
    }

    private void EnableItem()
    {
        triggerEvent.Invoke();
    }
}
