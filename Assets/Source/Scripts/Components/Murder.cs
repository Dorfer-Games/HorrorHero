using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Murder : MonoBehaviour
{
   
    private bool colission;
    private List<GameObject> colisionTpuched;

    private void Start()
    {
        colission = false;
        colisionTpuched = new List<GameObject>();
        
    }

    private void OnCollisionEnter(Collision other)
    {
        //gameStartAction?.Invoke(true);
        if (!colisionTpuched.Contains(other.gameObject))
        {
            colission = true;
            colisionTpuched.Add(other.gameObject);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        colission = false;
    }

    public bool colissionBool()
    {
        return colission;
    }
}
