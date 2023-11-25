using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiceAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private DiceRotation _Dice_Rotation;

    [SerializeField] private ParticleSystem _Finsh_Light;


    [SerializeField] private float _Punch_Duration;
    [SerializeField] private float _Increase_Duration;
    private Vector3 _Default_Scale;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_Dice_Rotation.IsRotate)
        {
            ScaleIncrease();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DefaultScale();
    }
    
    private void Awake()
    {
        _Dice_Rotation = GetComponent<DiceRotation>();
        
        GameEvents._Finish_Rotation += FinishAnimation;
    
        _Default_Scale = transform.localScale;
    
    }
    
    private void DefaultScale()
    {
        transform.DOScale(_Default_Scale, _Increase_Duration);
    }

    private void ScaleIncrease()
    {
        Vector3 _new_Scale = new Vector3(1.5f, 1.5f, 1.5f);
        transform.DOScale(_new_Scale, _Increase_Duration);
    }

    private void FinishAnimation()
    {
        int _vibrato = 0;
        float _elasticity = 0;

        Vector3 _punch_Force = new Vector3(0.5f, 0.5f, 0.5f);
        transform.DOPunchScale(_punch_Force, _Punch_Duration, _vibrato, _elasticity);
        
        DiceValueChangeParticle();

    }

    public void DiceHit(ParticleSystem _hit_Light)
    {
        _hit_Light.gameObject.SetActive(true);
    }

    public void DiceAddValueAnimation()
    {
        int _vibrato = 0;
        float _elasticity = 0;

        Vector3 _punch_Force = new Vector3(0, 0, 0.35f);
        transform.DOPunchPosition(_punch_Force, _Punch_Duration, _vibrato, _elasticity);
        
        DiceValueChangeParticle();
    }

    private void DiceValueChangeParticle()
    {
        float _offset = 1.1f;
        
        _Finsh_Light.transform.position = new Vector3(transform.position.x, transform.position.y + _offset, transform.position.z);
        _Finsh_Light.gameObject.SetActive(true);
    }
    
    private void OnDisable()
    {
        GameEvents._Finish_Rotation -= FinishAnimation;
    }

    

}
