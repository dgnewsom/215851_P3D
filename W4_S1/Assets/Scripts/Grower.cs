using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grower : MonoBehaviour
{
    [SerializeField] private string playerTag;
    //[SerializeField] private string hibernateTrigger;
    //[SerializeField] private string growTrigger;

    private Animator animator;
    private bool inTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (inTrigger)
        {
            animator.SetBool("Bloomed",true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("In here");
        if (other.tag == playerTag)
        {
            inTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Exiting here");
        if (other.tag == playerTag)
        {
            inTrigger = false;
            animator.SetBool("Bloomed", false);
        }
    }
}