using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class DayNightController : MonoBehaviour
{
    [SerializeField] private bool isDaytime = false;
    [SerializeField] private VolumeProfile dayProfile;
    [SerializeField] private VolumeProfile nightProfile;
    
    private Light[] lights;
    private Volume volume;
    private Animator animator;
    private float animationSpeed;

    private void Start()
    {
        animator = GetComponent<Animator>();
        volume = GetComponentInChildren<Volume>();
        lights = FindObjectsOfType<Light>();
        animationSpeed = animator.GetFloat("Speed");
        SetNighttime();
    }

    private void UpdateAnimator()
    {
        animator.SetBool("IsDaytime",isDaytime);
    }

    private void SetDaytime()
    {
        isDaytime = true;
        volume.profile = dayProfile;
        UpdateAnimator();
    }

    private void SetNighttime()
    {
        isDaytime = false;
        volume.profile = nightProfile;
        UpdateAnimator();
    }
    [ContextMenu("Toggle Day/Night")]
    public void ToggleDayNight()
    {
        isDaytime = !isDaytime;
        if (isDaytime)
        {
            SetDaytime();
        }
        else
        {
            SetNighttime();
        }
    }

    [ContextMenu("Skip To Day")]
    public void SkipToDaytime()
    {
        SetDaytime();
        animator.SetFloat("Speed",5f);
    }
    
    [ContextMenu("Skip To Night")]
    public void SkipToNighttime()
    {
        SetNighttime();
        animator.SetFloat("Speed",5f);
    }

    public void EnableHouseLights()
    {
        foreach (var light in lights)
        {
            if (light.gameObject.name != "Moon" && light.gameObject.name != "Sun")
            {
                light.enabled = true;
            }
        }
    }
    
    public void DisableHouseLights()
    {
        foreach (var light in lights)
        {
            if (light.gameObject.name != "Moon" && light.gameObject.name != "Sun")
            {
                light.enabled = false;
            }
        }
        
    }

    public void ResetSpeed()
    {
        animator.SetFloat("Speed",animationSpeed);
    }
}
