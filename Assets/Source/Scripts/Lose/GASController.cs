
using DG.Tweening;
using Kuhpik;
using TMPro;
using UnityEngine;

public class GASController : GameSystem, IIniting, IUpdating
{
    [SerializeField] private GameObject gas;

    public float angle;

    private float newAngle;
    private bool t;

    void IIniting.OnInit()
    {
        gas.SetActive(true);
        game.masking = true;
        Animator anim = game.victim.transform.GetChild(0).GetComponent<Animator>();
        Animator animMurder = game.murder.transform.GetChild(0).GetComponent<Animator>();
        anim.SetBool("Lose", true);
        animMurder.SetBool("Lose", true);
        EndRotate();
        t = false;

        Vector3 targetDir = game.murder.transform.position - game.victim.transform.position;
        Vector3 forw = game.victim.transform.forward;

        angle = Vector3.SignedAngle(targetDir, forw, Vector3.up);
        
        newAngle = -180 - angle;
        
        Vector3 newRotate = new Vector3(0, newAngle, 0);
        game.victim.transform.DORotate(newRotate, 0.45f).OnComplete(EndRotate);
        // //game.victim.transform.DORotate(-newRotate, 0.25f).OnComplete(EndRotate);
        //float angle = Vector3.Angle(game.victim.transform.position, game.murder.transform.position);
        // game.victim.transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
    }

    void EndRotate()
    {
        t = true;
        //Vector3 newRotate = new Vector3(0, angle, 0);
        //game.victim.transform.DORotate(newRotate, 0.25f);

    }

    void IUpdating.OnUpdate()
    {
        if (t)
        {
            game.victim.transform.rotation = Quaternion.Euler(new Vector3(0, newAngle, 0));
        }
    }
    
    
}
