
using DG.Tweening;
using Kuhpik;
using UnityEngine;

public class GASController : GameSystem, IIniting
{
    [SerializeField] private GameObject gas;

    public float angle1;
    public float angle2;
    public float angle3;
    public float angle4;
    public float angle5;
    public float angle6;
    public float angle7;

    void IIniting.OnInit()
    {
        gas.SetActive(true);
        EndRotate();
        Vector3 newRotate = new Vector3(0, -180, 0);
       // //game.victim.transform.DORotate(-newRotate, 0.25f).OnComplete(EndRotate);
        //float angle = Vector3.Angle(game.victim.transform.position, game.murder.transform.position);
       // game.victim.transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
    }

    void EndRotate()
    {
        angle1 = Vector3.SignedAngle(game.victim.transform.position, game.murder.transform.position, Vector3.zero);
        angle2 = Vector3.SignedAngle(game.victim.transform.position, game.murder.transform.position, Vector3.up);
        angle3 = Vector3.SignedAngle(game.victim.transform.position, game.murder.transform.position, Vector3.down);
        angle4 = Vector3.SignedAngle(game.victim.transform.position, game.murder.transform.position, Vector3.forward);
        angle5 = Vector3.SignedAngle(game.victim.transform.position, game.murder.transform.position, Vector3.back);
                
        //Vector3 newRotate = new Vector3(0, angle, 0);
        //game.victim.transform.DORotate(newRotate, 0.25f);
        
    }
}
