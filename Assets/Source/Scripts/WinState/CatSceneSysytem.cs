using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Kuhpik;
using UnityEngine;

public class CatSceneSysytem : GameSystem, IIniting
{
     private Camera cam;
     private Vector3 pos;
     
     void IIniting.OnInit()
     {
          game.murder.transform.DOScale(Vector3.one, 0.1f).SetEase(Ease.OutBounce);
          cam = Camera.main;
          pos = new Vector3(-0.9f, 6.78f, 125.39f);
          game.light.SetActive(true);
          Vector3 rot = new Vector3(11.31f, 0,0);
          Vector3 posVictim = game.victim.transform.position;
          posVictim.z -= 1.5f;
          game.murder.transform.DOMove(posVictim, 0.5f);
          cam.transform.DOMove(pos, 2f).OnComplete(EndRotate);
          cam.transform.DORotate(rot, 2f);
          
     }

     void EndRotate()
     {
          //pos.z = 131.93f;
          //cam.transform.DOMove(pos, 0.1f);
         // cam.orthographic = true;
          //game.victim.SetActive(false);
     }
}
