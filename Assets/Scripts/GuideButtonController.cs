using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideButtonController: MonoBehaviour
{
    public GameObject master;
    private Master masterMono;
    private Animator animator;
    private bool isTriggered;
    public AudioSource audioSource;

    public void Awake()
    {
        //gmc = guide.GetComponent<GuideMovementController>();
        masterMono = master.GetComponent<Master>();
        animator = GetComponent<Animator>();
        isTriggered = false;
    }
    

    void Update () 
	{
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Pressed"))
        {
            if(!isTriggered)
            {
                masterMono.BeginTryToLead();
                audioSource.Play();
                isTriggered = true;
            }
        }
        else
        {
            isTriggered = false;
            masterMono.EndLeading();

        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.ResetTrigger("Normal");
            animator.SetTrigger("Pressed");


        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            animator.ResetTrigger("Pressed");
            animator.SetTrigger("Normal");


        }
        

    }

}
