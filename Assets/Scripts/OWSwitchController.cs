using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OWSwitchController : MonoBehaviour {
    public GameObject controlled;
    private Animator ator;
    private OtherWorldController oc;

    public void Awake () {
        //isOn = false;
        //isActvated = false;
        ator = GetComponentInChildren<Animator> ();
        oc = controlled.GetComponent<OtherWorldController> ();

    }

    void Update () {

    }

    void OnTriggerEnter2D (Collider2D collider) {
        if (!ator.GetBool ("triggered")) {
            var colliderTag = collider.tag;
            if (colliderTag == "Master") {
                ator.SetBool ("activated", true);
                oc.PushAnswerQueue(gameObject);
            }

        }
    }

}