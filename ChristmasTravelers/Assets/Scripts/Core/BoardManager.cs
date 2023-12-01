using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoardCommands;

public class BoardManager : MonoBehaviour
{
    public static BoardManager instance;

    private BoardCommandHandler commandHandler;

    private void Awake()
    {
        instance = this;
        commandHandler = new BoardCommandHandler();
    }

    public void Execute(IBoardCommand command)
    {
        command.ExecuteOn(commandHandler);
    }
}
