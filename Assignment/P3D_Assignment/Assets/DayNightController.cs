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
    
    private LightBulb[] lights;
    private Volume volume;
    private Animator animator;
    private float animationSpeed;

    public bool IsDaytime => isDaytime;

    private void Start()
    {
        animator = GetComponent<Animator>();
        volume = GetComponentInChildren<Volume>();
        lights = FindObjectsOfType<LightBulb>();
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
        foreach (LightBulb light in lights)
        {
            light.enabled = true;
        }
    }
    
    public void DisableHouseLights()
    {
        foreach (LightBulb light in lights)
        {
            light.enabled = false;
        }
        
    }

    public void ResetSpeed()
    {
        animator.SetFloat("Speed",animationSpeed);
    }
}
