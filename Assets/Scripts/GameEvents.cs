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

    //Dice finish rotate
    public static void FinishRotation()
    {
        _Finish_Rotation?.Invoke();
    }

    //Finish current turn
    public static void FinishTurn()
    {
        _Finish_Turn?.Invoke();
    }

    //Start new turn
    public static void StartTurn()
    {
        _Start_Turn?.Invoke();
    }

    //Calculate dice value
    public static void TurnResult()
    {
        _Turn_Result?.Invoke();
    }
}
