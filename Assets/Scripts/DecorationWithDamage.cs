using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationWithDamage: MonoBehaviour
{
    public float damage = 1f;
    //public float forceMag = 100.0f;
    public float knockbackSpeed = 3.0f;

    public void Awake()
    {

    }
    

    void Update () 
	{


       
    }
    IEnumerator KnockBack(Rigidbody2D rig, Vector2 pos)
    {
        int count = 0;
        while (count != 6)
        {
            Vector2 between = rig.position - pos;
            Vector2 dir = between.normalized;
            //rig.AddForce(dir * forceMag);
            Vector2 newPos = rig.position + dir * Time.fixedDeltaTime * knockbackSpeed;
            rig.MovePosition(newPos);
            //rig.AddForce(dir * forceMag);
            yield return null;
            ++count;

        }
    }
    IEnumerator KnockBackProcess(Rigidbody2D rig, Vector2 pos)
    {
        Master moveController = rig.GetComponent<Master>();
        moveController.Freeze();
        StartCoroutine(KnockBack(rig, pos));
        yield return new WaitForSeconds(0.7f);
        moveController.UnFreeze();
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        var colliderTag = collision.collider.tag;
        //if (true)
        if (colliderTag == "Master")
        {
            Collider2D c = collision.collider;
            LivingEntity entity = c.GetComponent<LivingEntity>();
            entity.TakeHit(damage);

            Debug.Log(collision.collider.tag);
            Vector2 pos = transform.position;
            Rigidbody2D rig = collision.collider.attachedRigidbody;
            StartCoroutine(KnockBackProcess(rig, pos));
        }

    }
    
    

}
