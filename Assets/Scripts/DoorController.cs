using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {
    public GameObject switch_1;
    public GameObject switch_2;
    private Animator ator_d;
    private Animator ator_1;
    private Animator ator_2;
    private int triggeredNum;

    public enum DoorTypeEnum { Once1, Once2, Unlimited1, Unlimited2 }
    public DoorTypeEnum doorType;

    public void Awake () {
        triggeredNum = 0;
        ator_d = gameObject.GetComponentInChildren<Animator> ();
        if (switch_1 != null)
            ator_1 = switch_1.GetComponentInChildren<Animator> ();
        if (switch_2 != null)
            ator_2 = switch_2.GetComponentInChildren<Animator> ();
    }

    public void CheckStatus () {
        //Debug.Log(ator_1.GetCurrentAnimatorStateInfo(0).IsName("Activated"));
        //Debug.Log(ator_1.GetBool("activated"));
        triggeredNum = 0;
        if (ator_1 != null && ator_1.GetBool ("activated"))
            ++triggeredNum;
        if (ator_2 != null && ator_2.GetBool ("activated"))
            ++triggeredNum;
        switch (doorType) {

            case DoorTypeEnum.Once1:
                if (triggeredNum == 1) {
                    ator_d.SetBool ("activated", true);
                    OpenDoorOnce ();
                }
                break;

            case DoorTypeEnum.Once2:
                if (triggeredNum == 1) {
                    ator_d.SetBool ("activated", true);
                } else if (triggeredNum == 2) {
                    OpenDoorOnce ();
                } else if (triggeredNum == 0) {
                    ator_d.SetBool ("activated", false);
                }
                break;

            case DoorTypeEnum.Unlimited1:
                if (triggeredNum == 1) {
                    ator_d.SetBool ("activated", true);
                    OpenDoorUnlimited ();
                } else if (triggeredNum == 0) {
                    ator_d.SetBool ("activated", false);
                    CloseDoorUnlimited ();
                }
                break;
        }

    }

    void Update () {

    }

    IEnumerator OpenDoorCoro () {
        yield return new WaitForSeconds (1.0f);
        ator_d.SetBool ("opened", true);
        if (ator_1 != null) {
            ator_1.SetBool ("triggered", true);
        }
        if (ator_2 != null) {
            ator_2.SetBool ("triggered", true);
        }
        yield return new WaitForSeconds (1.0f);
        //Destroy(gameObject.GetComponent<Collider2d>());
        gameObject.active = false;

    }

    public void OpenDoorOnce () {
        //Destroy(gameObject);
        //gameObject.active = false;
        //gameObject.SetActive(false);
        StartCoroutine (OpenDoorCoro ());
    }

    public void OpenDoorUnlimited () {

    }

    public void CloseDoorUnlimited () {

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