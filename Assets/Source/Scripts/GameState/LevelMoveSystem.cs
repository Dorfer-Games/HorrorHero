using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Kuhpik;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class LevelMoveSystem : GameSystem, IIniting
{
    [SerializeField] private NavMeshAgent victimNavMeshAgent;
    [SerializeField] private Rigidbody murderMeshAgent;
    [SerializeField] private GameObject finish;
    [SerializeField] private float time;

    [SerializeField] private List<Transform> eyes;

    private float startTime;
    private Murder murderScript;

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

    void FixedUpdate()
    {
        if (Bootstrap.GetCurrentGamestate() == EGamestate.Game)
        {
            MoveMurder();
            if (time > 0 && !murderScript.colissionBool())
            {
                time -= 0.1f;
            }
            else
            {
                victimNavMeshAgent.isStopped = true;
                victimNavMeshAgent.enabled = false;
                SeeBackward();
            }
        }
    }

    private void MoveMurder()
    {
        Vector3 pos = murderMeshAgent.transform.position;
        pos.z = victimNavMeshAgent.transform.position.z - 3;
        murderMeshAgent.MovePosition(pos);
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
        victimNavMeshAgent.speed += 0.5f;
        victimNavMeshAgent.enabled = true;
        victimNavMeshAgent.isStopped = false;
        time = startTime;
        victimNavMeshAgent.SetDestination(finish.transform.position);
    }
}
