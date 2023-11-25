using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using Random = UnityEngine.Random;

public class TurnResult : MonoBehaviour
{
    public IntReactiveProperty ValueForCompare = new IntReactiveProperty(20);
    public string Result { get; private set; }
    

    private void Awake()
    {
        GameEvents._Start_Turn += StartValue;
        GameEvents._Turn_Result += CalculateResult;
    }

    private void StartValue()
    {
        int _min_Value = 1;
        int _max_Value = 21;
        
        ValueForCompare.Value = Random.Range(_min_Value, _max_Value);
    }

    private void CalculateResult()
    {
        if(DiceSides.DiceValue >= ValueForCompare.Value)
        {
            Result = "Successful";
        }
        else
        {
            Result = "Failure";
        }
        
        GameEvents.FinishTurn();
    }
    

    private void OnDisable()
    {
        GameEvents._Start_Turn -= StartValue;
        GameEvents._Turn_Result -= CalculateResult;
    }
}
