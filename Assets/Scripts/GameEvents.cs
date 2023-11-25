using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEvents
{
    public static Action _Start_Turn;
    public static Action _Finish_Rotation;
    public static Action _Turn_Result;
    public static Action _Finish_Turn;

    public static void FinishRotation()
    {
        _Finish_Rotation?.Invoke();
    }

    public static void FinishTurn()
    {
        _Finish_Turn?.Invoke();
    }

    public static void StartTurn()
    {
        _Start_Turn?.Invoke();
    }

    public static void TurnResult()
    {
        _Turn_Result?.Invoke();
    }
}
