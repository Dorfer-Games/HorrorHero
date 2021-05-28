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
        
    }

    private void OnCollisionEnter(Collision other)
    {
        //gameStartAction?.Invoke(true);
        if (!other.gameObject.tag.Contains("Floor"))
        {
            colission = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (!other.gameObject.tag.Contains("Floor"))
        {
            colission = false;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (!other.gameObject.tag.Contains("Floor"))
        {
            colission = false;
        }
    }

    public bool colissionBool()
    {
        return colission;
    }
}
