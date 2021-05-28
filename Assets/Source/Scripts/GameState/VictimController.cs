﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Kuhpik;
using UnityEngine;
using UnityEngine.AI;

public class VictimController : GameSystem, IIniting
{
    [SerializeField] private GameObject finish;
    [SerializeField] private float time;
    [SerializeField] private Transform znak;

    [SerializeField] private List<Transform> eyes;

    private float startTime;
    private Murder murderScript;
 
    private Transform victim;

    private Vector3 distance;
    private NavMeshAgent victimNavMeshAgent;
    
    void IIniting.OnInit()
    {
        startTime = time;
        victimNavMeshAgent = game.victim.GetComponent<NavMeshAgent>();
        victim = game.victim.transform;
        finish = GameObject.FindWithTag("EndLevel");
        victimNavMeshAgent.ActivateCurrentOffMeshLink(true);
        victimNavMeshAgent.SetDestination(finish.transform.position);
    }

    void FixedUpdate()
    {
        if (Bootstrap.GetCurrentGamestate() == EGamestate.Game)
        {
           
            if (time > 0 && !murderScript.colissionBool())
            {
                time -= 0.1f;
            }
            else
            {
                if (victimNavMeshAgent.enabled)
                {
                    victimNavMeshAgent.enabled = false;
                    Znak();
                }
            }
        }
    }

    private void Znak()
    {
        znak.localScale = new Vector3(1, 0.1f, 1);
        znak.DOScaleY(1, 0.3f).SetEase(Ease.OutBounce).OnComplete(SeeBackward);
    }
    

    private void SeeBackward()
    {
        Vector3 newRotate = new Vector3(0, 180, 0);
        victim.DORotate(newRotate, 0.2f).OnComplete(AnimationSee);
    }

    private void AnimationSee()
    {
        game.seeBackward = true;
        Vector3 eyePos = eyes[0].localPosition;
        eyePos.x = 0.05f;
        eyes[0].localPosition = eyePos;
        eyes[1].localPosition = eyePos;
        eyes[0].DOLocalMoveX(-0.05f, 0.5f).SetEase(Ease.Linear).SetLoops(3, LoopType.Yoyo);
        eyes[1].DOLocalMoveX(-0.05f, 0.5f).SetEase(Ease.Linear).SetLoops(3, LoopType.Yoyo).OnComplete(ReturnRptation);

    }

    private void ReturnRptation()
    {
        Vector3 newRotate = new Vector3(0, 0, 0);
        victim.DORotate(newRotate, 0.5f).OnComplete(Run);
    }

    private void Run()
    {
        game.seeBackward = false;
        victimNavMeshAgent.speed += 1;
        victimNavMeshAgent.enabled = true;
        time = startTime;
        victimNavMeshAgent.SetDestination(finish.transform.position);
    }
}
