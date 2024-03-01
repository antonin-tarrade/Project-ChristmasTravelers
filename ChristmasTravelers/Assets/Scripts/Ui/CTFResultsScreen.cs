using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTFResultsScreen : ResultsScreen
{
    public override void Display(Player[] rankedPlayers)
    {
        throw new System.NotImplementedException();
    }

    public override bool IsCompatible(GameModeData.Type type) => type == GameModeData.Type.CTF;

    protected override void OnCursorMovement(Vector2 m)
    {
        throw new System.NotImplementedException();
    }

    public override void OnScreenEntered()
    {
        base.OnScreenEntered();
    }

    protected override void OnScreenLeft()
    {
        base.OnScreenLeft();
    }
}
