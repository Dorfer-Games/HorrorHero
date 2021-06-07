using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Kuhpik;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class MainCamera : GameSystem, IIniting, IUpdating
{
    [SerializeField] private Camera cam;

    private Vector3 rotate;

    void IIniting.OnInit()
    {
        rotate = cam.transform.rotation.eulerAngles;
    }
    // Update is called once per frame
   void IUpdating.OnUpdate()
    {
        Vector3 pos = cam.transform.position;
        pos.z = game.murder.transform.position.z - 7;
        cam.transform.position = pos;

        rotate.y = game.murder.transform.rotation.eulerAngles.y;
        cam.transform.rotation = Quaternion.Euler(rotate); /**/
        //Vector3 targetPosition = murder.TransformPoint(new Vector3(0, 6, -7));
        //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

    }
}
