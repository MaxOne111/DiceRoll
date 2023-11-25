using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class BonusCard : MonoBehaviour
{
    [SerializeField] private int _Value;

    [SerializeField] private TextMeshProUGUI _Value_Text;
    
    [SerializeField] private ParticleSystem _Stars;
    [SerializeField] private float _Particles_Move_Speed;

    private Camera _Camera;

    private void Awake()
    {
        _Camera = Camera.main;
    }

    private void Start()
    {
        ShowValueText();
        StarsResetTransform();
    }

    public void Activate(Transform _dice)
    {
        StartCoroutine(AddValue(_dice));
    }

    private IEnumerator AddValue(Transform _dice)
    {
        _Stars.transform.localPosition = CardPosition(_dice);
        
        _Stars.Play();

        CardAnimation();
        
        while (_Stars.transform.localPosition != _dice.localPosition)
        {
            _Stars.transform.localPosition = Vector3.MoveTowards(_Stars.transform.localPosition, _dice.localPosition,
                _Particles_Move_Speed * Time.deltaTime);
            yield return null;
        }

        
        _Stars.transform.localPosition = CardPosition(_dice);
        
        int _dice_Value = DiceSides.DiceValue + _Value;

        _dice.transform.eulerAngles =  DiceSides.Side(_dice_Value);
        
        _dice.GetComponent<DiceAnimation>().DiceAddValueAnimation();
        
    }

    private Vector3 CardPosition(Transform _dice)
    {
        float distanceFromCamera = (_dice.position - _Camera.transform.position).magnitude;
        
        Vector3 _screen_Card_Pos = new Vector3(transform.position.x, transform.position.y, distanceFromCamera);
        
        Vector3 _world_Card_Position = _Camera.ScreenToWorldPoint(_screen_Card_Pos);

        return _world_Card_Position;
    }

    private void CardAnimation()
    {
        float _duration = 0.5f;
        int _vibrato = 0;
        float _elasticity = 0;
        
        Vector3 _punch_Force = new Vector3(0, -25, 0);

        transform.DOPunchPosition(_punch_Force, _duration, _vibrato, _elasticity);
    }

    private void ShowValueText()
    {
        _Value_Text.text = $"+{_Value.ToString()}";
    }

    private void StarsResetTransform()
    {
        _Stars.transform.parent = null;
        _Stars.transform.localScale = Vector3.one;
    }
    
}
