using BoardCommands;
using Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.InputSystem.InputAction;

public class CharacterController : MonoBehaviour
{
    private CharacterInput input;
    private MovementInput movementInput;

    public void Set(CharacterInput input)
    {
        this.input = input;
        movementInput = input.GetComponent<MovementInput>();
        input.Set(GetComponent<PlayerInput>());
    }

    public void Move(CallbackContext context)
    {
        Debug.Log(gameObject.name);
        movementInput.Move(context);
    }

    public void Grab(CallbackContext context)
    {
        input.Grab(context);
    }

    public void Shoot(CallbackContext context)
    {
        input.Shoot(context);
    }

    public void UseItem(CallbackContext context)
    {
        input.UseItem(context);
    }

    public void NextItem(CallbackContext context)
    {
        input.NextItem(context);
    }

    public void PreviousItem(CallbackContext context)
    {
        input.PreviousItem(context);
    }
}
