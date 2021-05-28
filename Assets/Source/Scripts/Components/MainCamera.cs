using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class MainCamera : MonoBehaviour
{
    public Transform murder;
    
    
    public float smoothTime = 0.5F;
    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame
   void Update()
    {
        Vector3 pos = transform.position;
        pos.z = murder.position.z - 7;
        transform.position = pos;/**/
        //Vector3 targetPosition = murder.TransformPoint(new Vector3(0, 6, -7));
        //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        
    }
}
