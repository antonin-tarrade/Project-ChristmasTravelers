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
    private InputAction input;
    private bool move;

    public void Move(InputAction.CallbackContext context){
        if (context.started || context.performed)
        {
            input = context.action;
            move = true;
        }
        if (context.canceled)
        {
            movement = Vector2.zero;
            move = false;
        }
    }

    public Vector3 GetMovementDirection()
    {
        return movement.normalized;
    }


    private void Update() {
        if (move) movement = input.ReadValue<Vector2>();
        if (movement.magnitude > 0) RequestCommand((new MoveBoardCommand(gameObject, speed * movement))); 
    }
}
