using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;

public class LevelLoadingSystem : GameSystem, IIniting
{
    [SerializeField] private string levelsPath;
    [SerializeField] private int maxLevels;
    void IIniting.OnInit()
    {
        game.victim = GameObject.FindWithTag("Victim");
        game.murder = GameObject.FindWithTag("Murder");         
        
        /*var levelGo = Resources.Load<GameObject>(string.Format(levelsPath, level + 1));
        
        Vibration.Init();
        game.level = Instantiate(levelGo);*/
    }
}
