using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    private CompositeDisposable _Disposable = new CompositeDisposable();
    
    [SerializeField] private TurnResult _Turn_Result;

    [SerializeField] private TextMeshProUGUI _Value_For_Compare_Text;
    [SerializeField] private TextMeshProUGUI _Result_Text;
    
    [SerializeField] private GameObject _Lock_Panel;
    
    [SerializeField] private Image _Click_To_Roll_Panel;

    private void Awake()
    {
        GameEvents._Start_Turn += LockScreen;
        GameEvents._Finish_Turn += UnlockScreen;
        
        GameEvents._Start_Turn += ClickToRollHide;
        GameEvents._Finish_Turn += ClickToRollShow;

        GameEvents._Finish_Turn += ShowResult;
    }

    private void Start()
    {
        UnlockScreen();
        ShowValueForCompare();
    }

    //Lock screen for ui activity
    private void LockScreen()
    {
        _Lock_Panel.SetActive(true);
    }

    //Unlock screen for ui activity
    private void UnlockScreen()
    {
        _Lock_Panel.SetActive(false);
    }

    //Show panel with text "Click to roll"
    private void ClickToRollShow()
    {
        float _duration = 0.5f;
        
        float _alpha = 1f;
        
        TextMeshProUGUI _text = _Click_To_Roll_Panel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        
        _Click_To_Roll_Panel.GetComponent<Image>().DOFade(_alpha, _duration);
        _text.DOFade(_alpha, _duration);
    }
    
    //Hide panel with text "Click to roll"
    private void ClickToRollHide()
    {
        float _duration = 0.5f;
        
        float _alpha = 0;
        
        TextMeshProUGUI _text = _Click_To_Roll_Panel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        
        _Click_To_Roll_Panel.DOFade(_alpha, _duration);
        _text.DOFade(_alpha, _duration);
    }

    //Show current value for compare
    private void ShowValueForCompare()
    {
        _Turn_Result.ValueForCompare.Subscribe(_ =>
        {
            _Value_For_Compare_Text.text = _Turn_Result.ValueForCompare.Value.ToString();

        }).AddTo(_Disposable);
    }

    //Show result text
    private void ShowResult()
    {
        Sequence _sequence = DOTween.Sequence();
        float _duration = 0.25f;
        float _delay = 1.5f;
            
        _Result_Text.text = _Turn_Result.Result;

        _sequence
            .Append(_Result_Text.DOFade(1f, _duration))
            .AppendInterval(_delay)
            .Append(_Result_Text.DOFade(0, _duration));
    }

  

    private void OnDisable()
    {
        _Disposable.Clear();
        
        GameEvents._Start_Turn -= LockScreen;
        GameEvents._Finish_Turn -= UnlockScreen;
        
        GameEvents._Start_Turn -= ClickToRollHide;
        GameEvents._Finish_Turn -= ClickToRollShow;
        
        GameEvents._Finish_Turn += ShowResult;
    }
}
