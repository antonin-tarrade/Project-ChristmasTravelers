using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Records;
using BoardCommands;
using System;
using UnityEngine.InputSystem;


public class MovementInput : SimpleInput
{
    private Vector2 movement;


    [SerializeField] private float speed;

    public void Move(InputAction.CallbackContext context){
        movement = context.ReadValue<Vector2>();
    }

    private void Update() {
        if (movement.magnitude > 0) RequestCommand((new MoveBoardCommand(gameObject, speed * movement))); 
    }
}
