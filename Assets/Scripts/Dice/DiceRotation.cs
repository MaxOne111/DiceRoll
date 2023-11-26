using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Dreamteck.Splines;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class DiceRotation : MonoBehaviour, IPointerDownHandler
{
    private CompositeDisposable _Disposable = new CompositeDisposable();

    private Vector3 _Direction;
    private Vector3 _Start_Rotation;
    private float _Rotate_Speed;
    public bool IsRotate { get; private set; }
    
    [SerializeField] private float _Max_Speed;
    [SerializeField] private float _Min_Speed;
    [SerializeField] private float _Lerp_Time;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!IsRotate)
        {
            GameEvents.StartTurn();
            Rotate();
        }
    }
    
    private void Awake()
    {
        GameEvents._Finish_Rotation += RotateDiceToSide;
        GameEvents._Finish_Turn += CanRotate;
    }

    private void Start()
    {
        _Rotate_Speed = _Max_Speed;

        _Start_Rotation = transform.eulerAngles;
    }

    //Dice rotation
    private void Rotate()
    {
        IsRotate = true;

        transform.eulerAngles = _Start_Rotation;
        transform.localScale = Vector3.one;
        
        Observable.EveryUpdate().Subscribe(_ =>
        {
            _Rotate_Speed = Mathf.Lerp(_Rotate_Speed, _Min_Speed, _Lerp_Time * Time.deltaTime);
            transform.Rotate(_Direction * _Rotate_Speed * Time.deltaTime);
            
        }).AddTo(_Disposable);
    }
    
    
    //Completion of rotation
    public void FinishRotation()
    {
        GameEvents.FinishRotation();
    }

    //Possibility of dice to rotate
    private void CanRotate()
    {
        IsRotate = false;
    }

    //Rotate dice to random side
    private void RotateDiceToSide()
    {
        _Disposable.Clear();
        
        transform.eulerAngles = DiceSides.Side(DiceSides.RandomValue());
        
        _Rotate_Speed = _Max_Speed;
    }
    
    //Direction of rotation in the direction of movement
    public void SetDirection(Transform _node)
    {
        _Direction = _node.position - transform.forward;
    }

    private void OnDisable()
    {
        GameEvents._Finish_Rotation -= RotateDiceToSide;
        GameEvents._Finish_Turn -= CanRotate;
        
        _Disposable.Clear();
    }

}
