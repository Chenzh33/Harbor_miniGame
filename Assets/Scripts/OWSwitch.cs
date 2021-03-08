using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OWSwitch : MonoBehaviour {
    public GameObject controlled;
    private Animator ator;
    private OWController oc;
    private bool isCurrent;

    public void Awake () {
        isCurrent = false;
        ator = GetComponentInChildren<Animator> ();
        oc = controlled.GetComponent<OWController> ();

    }

    void Update () {

    }

    void OnTriggerEnter2D (Collider2D collider) {
        if (isCurrent && !ator.GetBool ("triggered")) {
            var colliderTag = collider.tag;
            if (colliderTag == "Master") {
                ator.SetBool ("activated", true);
            }

        }
    }

    public void SetCurrent()
    {
        isCurrent = true;
    }

    public void RemoveCurrent()
    {
        isCurrent = false;
    }
}