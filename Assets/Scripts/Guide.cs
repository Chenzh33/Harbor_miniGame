using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (GuideMovementController))]
public class Guide : LivingEntity 
{

    public float guideMovementSpeed = 2f;
    private bool isFrozen;
    public Joystick joystick;

    private GuideMovementController guideController;

    public void Awake()
    {
        guideController = GetComponent<GuideMovementController>();
        isFrozen = false;
        Input.multiTouchEnabled = true;
        maxHealth = 5f;
        currHealth = maxHealth;
    }
    public void Freeze()
    {
        isFrozen = true;
    }

    public void UnFreeze()
    {
        isFrozen = false;
    }
    public void Update()
    {
      
    }

    public void FixedUpdate()
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        inputVector = inputVector * guideMovementSpeed;
        if(!isFrozen)
            guideController.Move(inputVector);

    }
}
