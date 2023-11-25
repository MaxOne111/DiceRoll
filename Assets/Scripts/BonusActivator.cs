using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusActivator : MonoBehaviour
{
    [SerializeField] private Transform _Dice;
    [SerializeField] private List<BonusCard> _Bonuses;
    
    private void Start()
    {
        GameEvents._Finish_Rotation += BonusActivate;
    }

    private void BonusActivate()
    {
        StartCoroutine(BonusActivateCoroutine());
    }

    private IEnumerator BonusActivateCoroutine()
    {
        float _delay = 1.5f;
        
        if (_Bonuses.Count > 0)
        {
            for (int i = 0; i < _Bonuses.Count; i++)
            {
                
                yield return new WaitForSeconds(_delay);
                
                if (DiceSides.DiceValue == 20)
                {
                    break;
                }
                _Bonuses[i].Activate(_Dice);
            }
        }
        
        yield return new WaitForSeconds(_delay);
        
        GameEvents.TurnResult();

    }

    private void OnDisable()
    {
        GameEvents._Finish_Rotation -= BonusActivate;
    }
}
