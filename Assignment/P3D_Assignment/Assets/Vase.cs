using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : MonoBehaviour
{
    [SerializeField] private GameObject vaseModel;
    [SerializeField] private GameObject brokenVaseModel;
    [SerializeField] private GameObject keyCollectible;

    private Material vaseMaterial;
    private float shaderProgress = 1;
    private bool isAppearing = false;
    private float smashForce = 9f;
    
    private void Update()
    {
        if (isAppearing)
        {
            shaderProgress = Mathf.Clamp01(shaderProgress - Time.deltaTime * 0.5f);
            if (shaderProgress <= 0)
            {
                isAppearing = false;
            }
            vaseMaterial.SetFloat("_Progress",shaderProgress);
        }
    }

    private void OnEnable()
    {
        vaseMaterial = vaseModel.GetComponentInChildren<Renderer>().material;
        shaderProgress = 1;
        vaseMaterial.SetFloat("_Progress",shaderProgress);
        isAppearing = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            if (other.relativeVelocity.x > smashForce || other.relativeVelocity.y > smashForce || other.relativeVelocity.z > smashForce)
            {
                SmashVase();
            } 
        }
    }

    private void SmashVase()
    {
        vaseModel.SetActive(false);
        GetComponent<Collider>().enabled = false;
        brokenVaseModel.SetActive(true);
        keyCollectible.SetActive(true);
    }
}
