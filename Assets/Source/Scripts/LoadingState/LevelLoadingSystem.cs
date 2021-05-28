using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoadingSystem : GameSystem, IIniting
{
    [SerializeField] private string levelsPath;
    [SerializeField] private int maxLevels;
    void IIniting.OnInit()
    {
        game.victim = GameObject.FindWithTag("Victim");
        game.murder = GameObject.FindWithTag("Murder");
        game.masking = true;
        game.fearBar = GameObject.FindWithTag("Fear").GetComponent<Scrollbar>();
        /*var levelGo = Resources.Load<GameObject>(string.Format(levelsPath, level + 1));
        
        Vibration.Init();
        game.level = Instantiate(levelGo);*/
    }
}
