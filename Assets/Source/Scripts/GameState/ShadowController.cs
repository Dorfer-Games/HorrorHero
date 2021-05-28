using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Kuhpik;
using UnityEngine;

public class ShadowController : GameSystem, IIniting, IUpdating
{
    [SerializeField] private Transform shadow;
    [SerializeField] private Transform shadowMask;

    private bool exactle;

    void IIniting.OnInit()
    {
        exactle = game.masking;
    }
    void IUpdating.OnUpdate()
    {
        if (!game.masking && exactle)
        {
            exactle = false;
            shadowMask.DOMoveZ(game.victim.transform.position.z + 1.8f, 0.5f);
        }

        if (game.masking && !exactle)
        {
            exactle = true;
            shadowMask.DOMoveZ(game.victim.transform.position.z -0.5f, 0.5f);
        }

        if (game.masking && exactle)
        {
            Vector3 pos = game.victim.transform.position;
            pos.z -= 0.5f;
            pos.y = shadowMask.transform.position.y;
            shadowMask.transform.position = pos;
            pos.z += 0.5f;
            pos.z -= 1.34f;
            shadow.transform.position = pos;
        }
    }
}
