using BoardCommands;
using Records;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public abstract class SimpleInput : MonoBehaviour, IRecordable
{
    public event Action<IBoardCommand> OnCommandRequest;
    public bool isActive;

    private void Awake()
    {
        isActive = true;
    }

    protected void RequestCommand(IBoardCommand command)
    {
        if (isActive && command != null) OnCommandRequest?.Invoke(command);
    }
}
