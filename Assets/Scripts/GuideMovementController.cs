using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideMovementController : MonoBehaviour {

    private Rigidbody2D rbodyGuide;
    private IsometricCharacterRenderer isoRenderer;

    private void Awake () {
        rbodyGuide = GetComponent<Rigidbody2D> ();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
    }

    public void Move (Vector2 movement) {
        Vector2 currentPosGuide = rbodyGuide.position;
        Vector2 newPos = currentPosGuide + movement * Time.fixedDeltaTime;
        isoRenderer.SetDirection(movement);
        rbodyGuide.MovePosition (newPos);
    }

}