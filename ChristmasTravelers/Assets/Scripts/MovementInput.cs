using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Records;
using BoardCommands;
using System;
using UnityEngine.InputSystem;


public class MovementInput : MonoBehaviour, IRecordable<MoveBoardCommand>
{
    public event Action<MoveBoardCommand> OnCommandRequest;



    [SerializeField] private float speed;

    public void Move(InputAction.CallbackContext context){
        Vector2 movement = context.ReadValue<Vector2>();
        if (movement.magnitude > 0) OnCommandRequest?.Invoke(new MoveBoardCommand(gameObject, speed * movement));
    }
}
