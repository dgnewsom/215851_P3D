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


    private void Start()
    {
        animator = GetComponent<Animator>();
        volume = GetComponentInChildren<Volume>();
        lights = FindObjectsOfType<Light>();
        SetNighttime();
    }

    public void UpdateAnimator()
    {
        animator.SetBool("IsDaytime",isDaytime);
    }

    internal void SetDaytime()
    {
        isDaytime = true;
        volume.profile = dayProfile;
    }

    internal void SetNighttime()
    {
        isDaytime = false;
        volume.profile = nightProfile;
    }

    public void ToggleDayNight()
    {
        isDaytime = !isDaytime;
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
}
