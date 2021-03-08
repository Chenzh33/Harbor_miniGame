using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileWithDamage: MonoBehaviour
{
    public float damage = 1f;

    public void Awake()
    {

    }
    

    void Update () 
	{


       
    }

  
    void OnTriggerEnter2D(Collider2D collider)
    {
        var colliderTag = collider.tag;
        if (colliderTag == "Master")
        {
            LivingEntity entity = collider.GetComponent<LivingEntity>();
            entity.TakeHit(damage);

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
