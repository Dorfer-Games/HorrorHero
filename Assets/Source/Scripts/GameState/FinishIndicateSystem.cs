using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Kuhpik;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class FinishIndicateSystem : GameSystem, IUpdating
{
    void IUpdating.OnUpdate()
    {
        if (game.seeBackward & game.masking)
        {
            Bootstrap.ChangeGameState(EGamestate.Lose);
        }

        if (game.fearBar.value >= 0.95f)
        {
            Bootstrap.ChangeGameState(EGamestate.Lose);
        }

        if (game.victim.transform.position.z >= game.finish.transform.position.z)
        {
            Bootstrap.ChangeGameState(EGamestate.Win);
        }
        
    }
}
