using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;
using UniRx;
using UnityEngine.EventSystems;

public class DiceMovement : MonoBehaviour
{
    private CompositeDisposable _Disposable = new CompositeDisposable();
    
    private SplineFollower _Follow;
    private bool _Is_Move;
    
    [SerializeField] private float _Min_Speed;
    [SerializeField] private float _Max_Speed;
    [SerializeField] private float _Lerp_Time;
    
    private void Awake()
    {
        _Follow = GetComponent<SplineFollower>();
        
        GameEvents._Finish_Rotation += FinishRotation;
        GameEvents._Start_Turn += SlowingDown;
    }
    
    private void Start()
    {
        _Follow.follow = false;
        _Follow.followSpeed = _Max_Speed;
    }

    private void SlowingDown()
    {
        _Follow.follow = true;

        Observable.EveryUpdate().Subscribe(_ =>
        {
            _Follow.followSpeed = Mathf.Lerp(_Follow.followSpeed, _Min_Speed, _Lerp_Time * Time.deltaTime);
            
        }).AddTo(_Disposable);
    }

    private void FinishRotation()
    {
        _Disposable.Clear();

        _Follow.followSpeed = _Max_Speed;
        _Follow.follow = false;
    }
    
    
    private void OnDisable()
    {
        GameEvents._Finish_Rotation -= FinishRotation;
        GameEvents._Start_Turn -= SlowingDown;
        
        _Disposable.Clear();
    }
    
}
