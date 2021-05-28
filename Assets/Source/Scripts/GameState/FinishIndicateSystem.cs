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
            /*FAIL*/
        }
        
    }
}
