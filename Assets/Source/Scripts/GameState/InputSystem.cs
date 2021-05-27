using System.Collections;
using System.Collections.Generic;
using Kuhpik;
using UnityEngine;

public class InputSystem : GameSystem, IUpdating
{
    void IUpdating.OnUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            /*отправляем RAY луч по X*/
            /*двигаем персонажа по Х*/
        }

        if (Input.GetMouseButtonUp(0))
        {
            /*маскируемся*/
        }
    }
}
