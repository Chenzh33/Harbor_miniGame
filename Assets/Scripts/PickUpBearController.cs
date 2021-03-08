using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBearController : MonoBehaviour {
    public void Awake () { }

    void Update () {

    }

    void OnTriggerEnter2D (Collider2D collider) {
        var colliderTag = collider.tag;
        if (colliderTag == "Master") {
            GameManager.Instance.PickUpBear();

        }

    }

}