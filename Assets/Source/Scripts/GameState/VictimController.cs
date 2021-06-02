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

    [SerializeField] private GameObject animGameObject;
    [SerializeField] private List<Transform> eyes;

    [SerializeField] private float firstStepAnim;
    [SerializeField] private float secondStepAnim;

    private float startTime;
    private Murder murderScript;
    private Animator anim;
    private Animator animMurder;
    private Transform victim;

    private Vector3 distance;
    private NavMeshAgent victimNavMeshAgent;
    private RectTransform screenPositions;
    private RectTransform questionPosition;
    private RectTransform watPosition;

    private Vector2 startHeightQuestion;
    private Vector2 startHeightWat;
    private float durationY;
    
    void IIniting.OnInit()
    {
        game.fear = false;
        startTime = time;
        victimNavMeshAgent = game.victimGhost.GetComponent<NavMeshAgent>();
        victimNavMeshAgent.enabled = true;
        victim = game.victim.transform;
        murderScript = game.murder.GetComponent<Murder>();
        victimNavMeshAgent.ActivateCurrentOffMeshLink(true);
        victimNavMeshAgent.SetDestination(game.finish.transform.position);
        znak.GetChild(0).gameObject.SetActive(false);
        znak.GetChild(1).gameObject.SetActive(false);
        
        screenPositions = znak.GetComponent<RectTransform>();
        questionPosition = znak.GetChild(0).GetComponent<RectTransform>();
        watPosition = znak.GetChild(1).GetComponent<RectTransform>();
        startHeightQuestion = questionPosition.sizeDelta;
        startHeightWat = watPosition.sizeDelta;
        
        durationY = screenPositions.position.y - Camera.main.WorldToScreenPoint(victim.position).y;
        anim = animGameObject.GetComponent<Animator>();
        animMurder = game.murder.transform.GetChild(0).GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (Bootstrap.GetCurrentGamestate() == EGamestate.Game)
        {
            if (time > 0 && !murderScript.colissionBool())
            {
                time -= 0.1f;
                Vector3 newPos = victimNavMeshAgent.transform.position;
                newPos.y = victim.transform.position.y;
                victim.transform.position = newPos;
                /*Vector3 rotate = victimNavMeshAgent.transform.rotation.eulerAngles;
                rotate.y *= 10;
                victim.transform.rotation = Quaternion.Euler(rotate);*/
                victim.transform.rotation = victimNavMeshAgent.transform.rotation;
            }
            else
            {
                if (victimNavMeshAgent.enabled)
                {
                    time = 0;
                    game.fear = true;
                    anim.SetBool("SeeBackward", true);
                    victimNavMeshAgent.enabled = false;
                    Znak();
                }
            }
        }
        else if (Bootstrap.GetCurrentGamestate() == EGamestate.Win)
        {
            /*маньяк бьет жертву ломом*/
            Vector3 newRotate = new Vector3(0, 180, 0);
            victim.DORotate(-newRotate, 0.25f);
            game.masking = true;
            anim.SetBool("Win", true);
            animMurder.SetBool("Win", true);
        }
        else if (Bootstrap.GetCurrentGamestate() == EGamestate.Lose)
        {
            Vector3 newRotate = new Vector3(0, 180, 0);
            victim.DORotate(-newRotate, 0.25f);
            game.masking = true;
            anim.SetBool("Lose", true);
            animMurder.SetBool("Lose", true);
            /*распыляем в дицо баллончик*/
        }
    }

    private void Znak()
    {
        znak.GetChild(0).gameObject.SetActive(true);
        Vector3 znakPose = Camera.main.WorldToScreenPoint(victim.position);
        znakPose.y += durationY;
        screenPositions.position = znakPose;
        //znak.DOScaleY(0.6f, timeFear).SetEase(Ease.OutBounce).OnComplete(SeeBackward);
        questionPosition.sizeDelta = Vector2.zero;
        questionPosition.DOSizeDelta(startHeightQuestion, timeFear).SetEase(Ease.OutBounce).OnComplete(SeeBackward);
    }
    

    private void SeeBackward()
    {
        znak.GetChild(0).gameObject.SetActive(false);
        Vector3 newRotate = new Vector3(0, 180, 0);
        znak.GetChild(1).gameObject.SetActive(true);
        watPosition.DOSizeDelta(startHeightWat, timeFear).SetEase(Ease.OutBounce).OnComplete(OffZnaks);
        victim.DORotate(-newRotate, 0.4f).OnComplete(AnimationSee);
    }

    private void AnimationSee()
    {
        watPosition.sizeDelta = Vector2.zero;
        game.seeBackward = true;
        Vector3 eyePos = eyes[0].localPosition;
        eyePos.x = 0.05f;
        eyes[0].localPosition = eyePos;
        eyes[1].localPosition = eyePos;
        eyes[0].DOLocalMoveX(-0.05f, 0.5f).SetEase(Ease.Linear).SetLoops(3, LoopType.Yoyo);
        eyes[1].DOLocalMoveX(-0.05f, 0.5f).SetEase(Ease.Linear).SetLoops(3, LoopType.Yoyo).OnComplete(ReturnRptation);

    }

    private void OffZnaks()
    {
        znak.GetChild(0).gameObject.SetActive(false);
        znak.GetChild(1).gameObject.SetActive(false);
    }

    private void ReturnRptation()
    {
        Vector3 newRotate = Vector3.zero;
        game.fear = false;
        game.seeBackward = false;
        victimNavMeshAgent.speed += 1.5f;
        
        if (victimNavMeshAgent.speed == firstStepAnim || victimNavMeshAgent.speed == secondStepAnim)
        {
            anim.SetFloat("Speed", 1);
            animMurder.SetFloat("Speed", 1);
        }
        else
        {
            anim.SetFloat("Speed", anim.GetFloat("Speed") +0.5f);
            animMurder.SetFloat("Speed", anim.GetFloat("Speed") +0.5f);
        }
        
        anim.SetBool("SeeBackward", false);
        anim.SetFloat("Run", victimNavMeshAgent.speed);
       
        victim.DORotate(newRotate, 0.5f).OnComplete(Run);
    }

    private void Run()
    {
        victimNavMeshAgent.enabled = true;
        time = startTime;
        victimNavMeshAgent.SetDestination(game.finish.transform.position);
    }
}
