using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;

public class MurderController : GameSystem, IIniting, IUpdating
{
    [SerializeField] private GameObject murder;

    private MeshRenderer murderMesh;
    
    void IIniting.OnInit()
    {
        murderMesh = murder.GetComponent<MeshRenderer>();
    }

    void IUpdating.OnUpdate()
    {
        murderMesh.enabled = game.masking;
    }
}
