using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Kuhpik;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class MainCamera : GameSystem, IUpdating
{
    [SerializeField] private Camera cam;
    // Update is called once per frame
   void IUpdating.OnUpdate()
    {
        Vector3 pos = cam.transform.position;
        pos.z = game.victim.transform.position.z - 10;
        cam.transform.position = pos;/**/
        //Vector3 targetPosition = murder.TransformPoint(new Vector3(0, 6, -7));
        //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        
    }
}
