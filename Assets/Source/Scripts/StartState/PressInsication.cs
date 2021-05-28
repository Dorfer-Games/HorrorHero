using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;

public class PressInsication : GameSystem, IUpdating
{
    void IUpdating.OnUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Bootstrap.ChangeGameState(EGamestate.Game);
        }
    }
}
