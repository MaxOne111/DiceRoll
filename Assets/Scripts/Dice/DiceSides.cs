using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public static class DiceSides
{

    private static Dictionary<int, Vector3> _Side;
    
    public static int DiceValue { get; private set; }

    //Initialize of dice sides positions
    public static void SidesInit()
    {
        _Side = new Dictionary<int, Vector3>()
        {
            [1] = new Vector3(-115, 130, -70),    [11] = new Vector3(-160,60,30),                  
            [2] = new Vector3(110, 176, -60),    [12] = new Vector3(145,-157,-101),                 
            [3] = new Vector3(-145,18,170),    [13] = new Vector3(-145,21,-13),                     
            [4] = new Vector3(160,-124,33),    [14] = new Vector3(144,-161,72),                     
            [5] = new Vector3(180,-30,-35),    [15] = new Vector3(180,32,-78),                    
            [6] = new Vector3(180,148,100),    [16] = new Vector3(0,30,-37),                    
            [7] = new Vector3(-147,-26,-100),    [17] = new Vector3(-160,-60,-148),                    
            [8] = new Vector3(140,155,165),    [18] = new Vector3(145,157,-15),                    
            [9] = new Vector3(-145,-25,78),    [19] = new Vector3(-70,-186,-54),
            [10] = new Vector3(160,117,-150),   [20] = new Vector3(68,118,-60),                       
            
        };
    }

    //Random dice value
    public static int RandomValue()
    {
        int _index = Random.Range(1, _Side.Count);
        return _index;
    }

    //Get of dice side position
    public static Vector3 Side(int _side_Value)
    {
        if (_Side.ContainsKey(_side_Value))
        {
            DiceValue = _side_Value;
            return _Side[_side_Value];
        }
        else
        {
            DiceValue = 20;
            return _Side[20];
        }
        
    }
}
