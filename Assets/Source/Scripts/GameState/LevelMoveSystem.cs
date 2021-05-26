using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Kuhpik;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class LevelMoveSystem : GameSystem, IIniting, IUpdating
{
    [SerializeField] private NavMeshAgent victimNavMeshAgent;
    [SerializeField] private Rigidbody murderMeshAgent;
    [SerializeField] private GameObject finish;
    [SerializeField] private float time;

    [SerializeField] private List<Transform> eyes;

    private float startTime;
    private Murder murderScript;
    private float previeSpeed;

    private Transform victim;

    private Vector3 distance;
    
    void IIniting.OnInit()
    {
        startTime = time;
        murderScript = murderMeshAgent.GetComponent<Murder>();
        victim = victimNavMeshAgent.transform;
        finish = GameObject.FindWithTag("EndLevel");
        victimNavMeshAgent.ActivateCurrentOffMeshLink(true);
        victimNavMeshAgent.SetDestination(finish.transform.position);
    }

    void IUpdating.OnUpdate()
    {
        MoveMurder();
        if (time > 0 && !murderScript.colissionBool())
        {
            time -= 0.1f;
        }
        else
        {
            time = 0;
            previeSpeed = victimNavMeshAgent.speed;
            victimNavMeshAgent.speed = 0;
            SeeBackward();
        }
    }

    private void MoveMurder()
    {
        Vector3 pos = murderMeshAgent.transform.position;
        pos.z = victimNavMeshAgent.transform.position.z - 3;
        murderMeshAgent.position = pos;
    }

    private void SeeBackward()
    {
        Vector3 newRotate = new Vector3(0, 180, 0);
        victim.DORotate(newRotate, 0.2f).OnComplete(AnimationSee);
    }

    private void AnimationSee()
    {
        Vector3 eyePos = eyes[0].localPosition;
        eyePos.x = 0.05f;
        eyes[0].localPosition = eyePos;
        eyes[1].localPosition = eyePos;
        eyes[0].DOLocalMoveX(-0.05f, 0.5f).SetEase(Ease.Linear).SetLoops(3);
        eyes[1].DOLocalMoveX(-0.05f, 0.5f).SetEase(Ease.Linear).SetLoops(3).OnComplete(ReturnRptation);
    }

    private void ReturnRptation()
    {
        Vector3 newRotate = new Vector3(0, 0, 0);
        victim.DORotate(newRotate, 0.5f).OnComplete(Run);
    }

    private void Run()
    {
        previeSpeed += 0.5f;
        victimNavMeshAgent.speed = previeSpeed;
        time = startTime;
    }
}
