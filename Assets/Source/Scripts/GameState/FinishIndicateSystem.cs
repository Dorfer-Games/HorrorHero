using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Kuhpik;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class FinishIndicateSystem : GameSystem, IIniting,IUpdating
{
    private NavMeshAgent victimAgent;
    void IIniting.OnInit()
    {
        victimAgent = game.victimGhost.GetComponent<NavMeshAgent>();
    }
    void IUpdating.OnUpdate()
    {
        if (game.seeBackward & game.masking)
        {
            victimAgent.enabled = false;
            Bootstrap.ChangeGameState(EGamestate.Lose);
        }

        if (game.fearBar.value >= 0.95f)
        {
            victimAgent.enabled = false;
            Bootstrap.ChangeGameState(EGamestate.Lose);
        }

        if (game.victim.transform.position.z >= game.finish.transform.position.z-1)
        {
            victimAgent.enabled = false;
            Bootstrap.ChangeGameState(EGamestate.Win);
        }
        
    }
}
