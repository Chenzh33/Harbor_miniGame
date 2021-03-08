using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (MasterMovementController))]
public class Master : LivingEntity 
{

    public float masterMovementSpeed = 2f;
    public float minDistBetweenGuideAMaster = 1.5f;
    public float maxGuideRangeToExit = 6.5f;
    public float maxGuideRangeToEnter = 5f;
    private bool isFrozen;
    public GameObject guide;

    private MasterMovementController masterController;
    private bool isLeading;

    public void Awake()
    {
        masterController = GetComponent<MasterMovementController>();
        isLeading = false;
        isFrozen = false;
        maxHealth = 10f;
        currHealth = maxHealth;
    }

    /* 
        private void CheckGame()
        {
            if(isDead)
            {
                Time.timeScale = 0;


            }

        }
        */
    public void Freeze()
    {
        isFrozen = true;
        isLeading = false;
    }

    public void UnFreeze()
    {
        isFrozen = false;
    }
    public void Update()
    {
        //CheckGame();
        
    }
   
    public void FixedUpdate()
    {

        Vector2 movement = Vector2.zero;
        if (isLeading)
        {
            
            Vector2 currentPosMaster = transform.position;
            Vector2 currentPosGuide = guide.transform.position;
            Vector2 between = currentPosGuide - currentPosMaster;
            Vector2 dir = between.normalized;
            float dist = between.magnitude;
            //Debug.Log(dist);
            if (dist > maxGuideRangeToExit)
                EndLeading();
            if (dist > minDistBetweenGuideAMaster)
            {
                movement = dir * masterMovementSpeed;
            }
        }
        if(!isFrozen)
            masterController.Move(movement);
    }

    public void BeginTryToLead()
    {
        Vector2 currentPosMaster = transform.position;
        Vector2 currentPosGuide = guide.transform.position;
        Vector2 between = currentPosGuide - currentPosMaster;
        float dist = between.magnitude;
        if (dist < maxGuideRangeToEnter)
            isLeading = true;
    }


    public void EndLeading()
    {
        isLeading = false;
    }
}
