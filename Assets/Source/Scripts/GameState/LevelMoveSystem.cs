using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class LevelMoveSystem : GameSystem, IIniting, IUpdating
{
    [SerializeField] private NavMeshAgent victimNavMeshAgent;
    [SerializeField] private NavMeshAgent murderMeshAgent;
    [SerializeField] private GameObject finish;

    private Vector3 distance;
    
    void IIniting.OnInit()
    {
        finish = GameObject.FindWithTag("EndLevel");
        victimNavMeshAgent.SetDestination(finish.transform.position);
        murderMeshAgent.SetDestination(victimNavMeshAgent.transform.position);
       murderMeshAgent.speed = victimNavMeshAgent.speed;
    }

    void IUpdating.OnUpdate()
    {
       murderMeshAgent.destination = victimNavMeshAgent.transform.position;
       murderMeshAgent.speed = victimNavMeshAgent.speed;
    }
}
