using BoardCommands;
using Records;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SimpleInput : MonoBehaviour, IRecordable
{
    public event Action<IBoardCommand> OnCommandRequest;

    protected void RequestCommand(IBoardCommand command)
    {
        OnCommandRequest?.Invoke(command);
    }
}
