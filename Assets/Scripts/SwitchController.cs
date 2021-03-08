using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwitchController : MonoBehaviour
{
    //public enum SwitchEnum { Once, Unlimited };
    //public SwitchEnum switchType;
    public GameObject controlled;
    private Animator ator;
    //private bool isOn;
    //private bool isActvated;
    private int numTriggered;
    private DoorController dc;
    public AudioSource audioSourceB;

    public void Awake()
    {
        //isOn = false;
        //isActvated = false;
        numTriggered = 0;
        ator = GetComponentInChildren<Animator>();
        dc = controlled.GetComponent<DoorController>();

    }
    

    void Update () 
	{
        /*
        if(numTriggered > 0)
        {
            DoorController dc = controlled.GetComponent<DoorController>();
            dc.OpenDoor();
            isOn = true;
        }
        else
        {
            if (isOn)
            {
                DoorController dc = controlled.GetComponent<DoorController>();
                dc.CloseDoor();
                isOn = false;

            }
       }
       */

       
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(!ator.GetBool("triggered"))
        {
            var colliderTag = collider.tag;
            if (colliderTag == "Master" || colliderTag == "Guide")
            {
                ++numTriggered;
                if (numTriggered > 0)
                {
                    ator.SetBool("activated", true);
                    dc.CheckStatus();
                    audioSourceB.PlayDelayed(1);
                }
            }

        }
    }


    void OnTriggerExit2D(Collider2D collider)
    {
        if (!ator.GetBool("triggered"))
        {
            var colliderTag = collider.tag;
            if (colliderTag == "Master" || colliderTag == "Guide")
            {
                --numTriggered;
                if (numTriggered == 0)
                {
                    ator.SetBool("activated", false);
                    dc.CheckStatus();
                }
            }

        }
    }
    /*
         void OnCollisionEnter2D(Collision2D collision)
         {
             var colliderTag = collision.collider.tag;
             if (colliderTag == "Master")
             {
                 Collider2D c = collision.collider;
                 LivingEntity entity = c.GetComponent<LivingEntity>();
                 entity.TakeHit(damage);

             }
             Debug.Log(collision.collider.tag);

         }
         */


}
