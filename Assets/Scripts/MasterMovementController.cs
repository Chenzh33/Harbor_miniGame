using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterMovementController : MonoBehaviour
{

    private Rigidbody2D rbodyMaster;
    private IsometricCharacterRenderer isoRenderer;

    public void Awake()
    {
        rbodyMaster = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
    }

    public void Move(Vector2 movement)
    {
        Vector2 currentPosMaster = rbodyMaster.position;
        Vector2 newPos = currentPosMaster + movement * Time.fixedDeltaTime;
        isoRenderer.SetDirection(movement);
        rbodyMaster.MovePosition(newPos);

    }
}
