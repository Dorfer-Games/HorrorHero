using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using Kuhpik;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class MurderController : GameSystem, IIniting, IUpdating
{
    [SerializeField] private Scrollbar controller;
    [SerializeField] private float maxX;
    [SerializeField] private GameObject animGameObject;
    [SerializeField] private Animator victimAnim;
    [SerializeField] private Transform objectBone;
    
    private float xPosition;
    private Vector3 murderPos;
    private Collider murderCollider;
    private bool exactle;
    private Animator anim;
    private NavMeshAgent victimAgent;

    private MeshRenderer murderMesh;

    private float previeDistance;
    private float currentdistance;

    private float otherSpeed;
    private float nornalSpeed;
    private Murder murderScript;

    void IIniting.OnInit()
    {
        murderCollider = game.murder.GetComponent<Collider>();
        murderPos = game.murder.transform.position;
        exactle = true;
        anim = animGameObject.GetComponent<Animator>();
        victimAgent = game.victimGhost.GetComponent<NavMeshAgent>();
        previeDistance = 3;
        currentdistance = previeDistance;
        nornalSpeed = anim.GetFloat("Speed");
        otherSpeed = nornalSpeed * 2;
        game.murder.transform.GetChild(1).gameObject.SetActive(false);
        murderScript = game.murder.GetComponent<Murder>();
    }

    void IUpdating.OnUpdate()
    {
        if (murderScript.colissionBool() && player.vibration)
        {
            Vibration.Vibrate(40);
        }

        if (murderScript.RotateBool() && victimAgent.enabled)
        {
            game.murder.transform.rotation = game.victim.transform.rotation;
        }
        else
        {
            game.murder.transform.DORotate(Vector3.zero, 2f);
        }
        
        if (game.masking)
         {
            murderPos = game.murder.transform.position;
            xPosition = controller.value;
            
            if (!exactle)
            {
                exactle = true;
                game.murder.transform.GetChild(1).gameObject.SetActive(false);
                anim.SetFloat("Run", victimAgent.speed);
                murderCollider.enabled = game.masking;
                game.murder.transform.GetChild(0).DOScale(Vector3.one, 0.45f);
            }

            murderPos.x = (xPosition - 0.5f) * maxX / 0.5f;
            murderPos.z = game.victim.transform.position.z - currentdistance;
            game.murder.transform.position = murderPos;

            if (currentdistance > previeDistance)
            {
                currentdistance -= 0.07f;
                anim.SetFloat("Speed", otherSpeed);
            }
            else
            {
                currentdistance = previeDistance;
                anim.SetFloat("Speed", nornalSpeed);

            }

            if (victimAgent.enabled)
            {
                anim.SetBool("Stop", false);
            }
            else
            {
                anim.SetBool("Stop", true);
            }
             
         }
         else
         {
            if (exactle)
            {
                murderPos = game.murder.transform.position;
                murderCollider.enabled = game.masking;
                exactle = false;
                nornalSpeed = victimAnim.GetFloat("Speed");
                otherSpeed = nornalSpeed * 1.5f;
                game.murder.transform.GetChild(0).DOScale(Vector3.zero, 0.45f);
            }
           game.murder.transform.GetChild(1).gameObject.SetActive(true);
            currentdistance = game.victim.transform.position.z - game.murder.transform.position.z;
            game.murder.transform.position = murderPos;
            Vector3 posBone = objectBone.position;
            posBone.z = game.victim.transform.position.z - 1.8f;
            posBone.x = game.victim.transform.position.x;
            objectBone.position = posBone;
         }
    }
}

