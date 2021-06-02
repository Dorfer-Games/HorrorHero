using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Kuhpik;
using UnityEngine;

public class CatSceneSysytem : GameSystem, IIniting, IUpdating
{
     private Camera cam;
     private Vector3 pos;
     private Light light;

     [SerializeField] private Transform gameCoordinats;
     
     void IIniting.OnInit()
     {
          game.murder.transform.DOScale(Vector3.one, 0.1f).SetEase(Ease.OutBounce);
          cam = Camera.main;
          pos = new Vector3(-0.9f, 6.1f, 126.5f);
          //game.light.SetActive(true);
          Vector3 rot = new Vector3(9f, 0,0);
          Vector3 posVictim = game.victim.transform.position;
          posVictim.z -= 1.5f;
          game.murder.transform.DOMove(posVictim, 0.5f);
          cam.transform.DOMove(pos, 3f).OnComplete(EndRotate);
          cam.transform.DORotate(rot, 3f);
          gameCoordinats.DOMoveX(13, 6f);
          game.light.transform.DOMoveY(0, 3f);
          light = game.light.GetComponent<Light>();
     }

     void EndRotate()
     {
          //pos.z = 131.93f;
          //cam.transform.DOMove(pos, 0.1f);
         // cam.orthographic = true;
          //game.victim.SetActive(false);
     }
     
     void IUpdating.OnUpdate()
     {
          light.intensity = gameCoordinats.position.x;
          
     }
}
