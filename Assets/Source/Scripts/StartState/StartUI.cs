using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : UIScreen
{
    [SerializeField] private Button tap;
    //[SerializeField] private Image tutorial;
    public override void Subscribe()
    {
        base.Subscribe();
        tap.onClick.AddListener(() => Bootstrap.ChangeGameState(EGamestate.Game));
    }
}
