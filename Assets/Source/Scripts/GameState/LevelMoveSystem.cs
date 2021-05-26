using System.Collections;
using System.Collections.Generic;
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

    private float startTime;

    private Vector3 distance;
    
    void IIniting.OnInit()
    {
        startTime = time;
        finish = GameObject.FindWithTag("EndLevel");
        victimNavMeshAgent.ActivateCurrentOffMeshLink(true);
        victimNavMeshAgent.SetDestination(finish.transform.position);
    }

    void IUpdating.OnUpdate()
    {
        MoveMurder();
        if (time > 0)
        {
            time -= 0.1f;
        }
    }

    private void MoveMurder()
    {
        Vector3 pos = murderMeshAgent.transform.position;
        pos.z = victimNavMeshAgent.transform.position.z - 3;
        murderMeshAgent.position = pos;
    }
}
