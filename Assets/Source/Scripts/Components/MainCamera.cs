using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform murder;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.z = murder.position.z - 7;
        transform.position = pos;
    }
}
