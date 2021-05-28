using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Kuhpik;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class MurderController : GameSystem, IIniting, IUpdating
{
    [SerializeField] private Scrollbar controller;

    [SerializeField] private float maxX;
    private float xPosition;
    private Vector3 murderPos;
    private Rigidbody murderRigidBody;

    private MeshRenderer murderMesh;
    
    void IIniting.OnInit()
    {
        murderRigidBody = game.murder.GetComponent<Rigidbody>();
        murderMesh = game.murder.GetComponent<MeshRenderer>();
        murderPos = game.murder.transform.position;
    }

    void IUpdating.OnUpdate()
    {
        murderMesh.enabled = game.masking;

        xPosition = controller.value;
        murderPos = game.murder.transform.position;

        murderPos.x = (xPosition-0.5f)*maxX/0.5f;
        murderPos.z = game.victim.transform.position.z - 3;

        murderRigidBody.MovePosition(murderPos);

    }
}
