using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using Kuhpik;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class MurderController : GameSystem, IIniting, IUpdating
{
    [SerializeField] private Scrollbar controller;
    [SerializeField] private float maxX;
    [SerializeField] private Transform shadow;
    [SerializeField] private Transform shadowMask;
    private float xPosition;
    private Vector3 murderPos;
    private Rigidbody murderRigidBody;
    private Collider murderCollider;

    private MeshRenderer murderMesh;
    
    void IIniting.OnInit()
    {
        murderCollider = game.murder.GetComponent<Collider>();
        murderRigidBody = game.murder.GetComponent<Rigidbody>();
        murderMesh = game.murder.GetComponent<MeshRenderer>();
        murderPos = game.murder.transform.position;
    }

    void IUpdating.OnUpdate()
    {
        murderMesh.enabled = game.masking;
        murderCollider.enabled = game.masking;

        xPosition = controller.value;
        murderPos = game.murder.transform.position;

        if (game.masking)
        {
            murderPos.x = (xPosition-0.5f)*maxX/0.5f;
            murderPos.z = game.victim.transform.position.z - 3;

            murderRigidBody.DOMove(murderPos, 0.5f);
        }

    }
}
