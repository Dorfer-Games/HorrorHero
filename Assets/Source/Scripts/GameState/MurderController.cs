using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using Kuhpik;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class MurderController : GameSystem, IIniting, IUpdating
{
    [SerializeField] private Scrollbar controller;
    [SerializeField] private float maxX;
    [SerializeField] private GameObject animGameObject;
    
    private float xPosition;
    private Vector3 murderPos;
    private Collider murderCollider;
    private bool exactle;
    private Animator anim;
    private NavMeshAgent victimAgent;

    private MeshRenderer murderMesh;

    private float previeDistance;
    private float currentdistance;

    void IIniting.OnInit()
    {
        murderCollider = game.murder.GetComponent<Collider>();
        murderPos = game.murder.transform.position;
        exactle = true;
        anim = animGameObject.GetComponent<Animator>();
        victimAgent = game.victimGhost.GetComponent<NavMeshAgent>();
        previeDistance = 3;
        currentdistance = previeDistance;
    }

    void IUpdating.OnUpdate()
    {
        //murderMesh.enabled = game.masking;
        murderCollider.enabled = game.masking;
        xPosition = controller.value;
        murderPos = game.murder.transform.position;

        if (game.masking)
        {
            if (!exactle)
            {
                exactle = true;
                anim.SetFloat("Run", victimAgent.speed);
                game.murder.transform.GetChild(0).DOScale(Vector3.one, 0.45f).SetEase(Ease.OutBounce);
            }

            murderPos.x = (xPosition - 0.5f) * maxX / 0.5f;
            murderPos.z = game.victim.transform.position.z - currentdistance;
            game.murder.transform.position = murderPos;

            if (currentdistance > previeDistance)
            {
                currentdistance -= 0.1f;
            }

        }
        else
        {
            if (exactle)
            {
                exactle = false;
                game.murder.transform.GetChild(0).DOScale(Vector3.zero, 0.45f).SetEase(Ease.OutBounce);
            }
            currentdistance = game.victim.transform.position.z - game.murder.transform.position.z;
        }

    }
}

