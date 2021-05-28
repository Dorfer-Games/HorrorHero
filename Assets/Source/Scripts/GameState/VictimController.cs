using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Kuhpik;
using UnityEngine;
using UnityEngine.AI;

public class VictimController : GameSystem, IIniting
{
    [SerializeField] private float time;
    [SerializeField] private Transform znak;
    [SerializeField] private float timeFear;

    [SerializeField] private List<Transform> eyes;

    private float startTime;
    private Murder murderScript;
 
    private Transform victim;

    private Vector3 distance;
    private NavMeshAgent victimNavMeshAgent;
    
    void IIniting.OnInit()
    {
        game.fear = false;
        startTime = time;
        victimNavMeshAgent = game.victimGhost.GetComponent<NavMeshAgent>();
        victim = game.victim.transform;
        murderScript = game.murder.GetComponent<Murder>();
        victimNavMeshAgent.ActivateCurrentOffMeshLink(true);
        victimNavMeshAgent.SetDestination(game.finish.transform.position);
        znak.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        /**/if (Bootstrap.GetCurrentGamestate() == EGamestate.Game)
        {
            if (time > 0 && !murderScript.colissionBool())
            {
                time -= 0.1f;
                victim.transform.position = victimNavMeshAgent.transform.position;
            }
            else
            {
                if (victimNavMeshAgent.enabled)
                {
                    game.fear = true;
                    victimNavMeshAgent.enabled = false;
                    Znak();
                }
            }
        }
    }

    private void Znak()
    {
        znak.gameObject.SetActive(true);
        znak.localScale = new Vector3(1, 0.1f, 1);
        znak.DOScaleY(1, timeFear).SetEase(Ease.OutBounce).OnComplete(SeeBackward);
    }
    

    private void SeeBackward()
    {
        znak.gameObject.SetActive(false);
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
        Vector3 newRotate = Vector3.zero;
        game.fear = false;
        victim.DORotate(newRotate, 0.5f).OnComplete(Run);
    }

    private void Run()
    {
        game.seeBackward = false;
        victimNavMeshAgent.speed += 1;
        victimNavMeshAgent.enabled = true;
        time = startTime;
        victimNavMeshAgent.SetDestination(game.finish.transform.position);
    }
}
