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
          Vector3 posVictim = game.victim.transform.position;
          posVictim.z -= 2f;
          Vector3 rot = new Vector3(9f, 0,0);
          cam.transform.DOMove(pos, 3f);
          cam.transform.DORotate(rot, 3f);
          gameCoordinats.DOMoveX(13, 6f).OnComplete(EndRotate);
          game.light.transform.DOMoveY(0, 3f);
          light = game.light.GetComponent<Light>();
          
          Vector3 newRotate = new Vector3(0, 180, 0);
          game.victim.transform.DORotate(-newRotate, 0.25f);
          game.masking = true;
          game.murder.transform.DOMove(posVictim, 0.5f).OnComplete(EndMurderPosition); /*После перемещения запускаем анимации*/
     }

     void EndRotate()
     {
          Vibration.VibratePeek();
          Bootstrap.ChangeGameState(EGamestate.WinScreen);
     }
     
     void IUpdating.OnUpdate()
     {
          light.intensity = gameCoordinats.position.x;
          
     }

     void EndMurderPosition()
     {
          Animator anim = game.victim.transform.GetChild(0).GetComponent<Animator>();
          Animator animMurder = game.murder.transform.GetChild(0).GetComponent<Animator>();
          anim.SetBool("Win", true);
          animMurder.SetBool("Win", true);
     }
}
