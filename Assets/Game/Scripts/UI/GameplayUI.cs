using System;
using System.Collections;
using System.Collections.Generic;
using Assets.ProgressBars.Scripts;
using DG.Tweening;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : Singleton<GameplayUI>
{
    [Header("-----UI-----"), Space(20f)] 
    [SerializeField] private TMP_Text _timerTxt;
    [SerializeField] private Image _timerImg;
    [SerializeField] private GameObject _tick;


    [Header("-----"), Space(10f)] 
    [SerializeField] private TMP_Text _lvTxt;

    [SerializeField] private GuiProgressBarUI _slider;
    [SerializeField] private Text _starTxt;
    [SerializeField] private WinPopUp _winUI;

    [SerializeField] private DOTweenAnimation _anim;
    [SerializeField] private GameObject _timer;
    [SerializeField] private GameObject _tipsBtn;
    
    
    [SerializeField] private float _width;
    [SerializeField] private RectTransform[] _milestoneImg;
    
    private float _oriAmount;

    private void OnValidate()
    {
        _width = _slider.GetComponent<RectTransform>().sizeDelta.x;
    }

    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            _milestoneImg[i].anchoredPosition =
                new Vector2(GameplayManager.Instance.StarMilestonePercentage[i] / 100 * _width, 0);
        }
        
        _oriAmount = GameplayManager.Instance.DrawAmount;
        GameplayManager.Instance.Win += WinUI;
        GameplayManager.Instance.Lose += LoseUI;
        GameplayManager.Instance.BeginDraw += BeginDraw;
        GameplayManager.Instance.EndDraw += EndDraw;
        _lvTxt.text = $"Level {DataManager.Instance.Level}";
    }

    public void UpdateSlider()
    {
        _slider.Value = GameplayManager.Instance.DrawAmount / _oriAmount;
        _starTxt.text = GameplayManager.Instance.GetCurrentMilestone().ToString();
    }

    private void WinUI()
    {
        StartCoroutine(Delay());
        IEnumerator Delay()
        {
            yield return new WaitForSeconds(1);
            _winUI.OpenPopUp();
        }
    }

    private void LoseUI()
    {
        StartCoroutine(Delay());
        IEnumerator Delay()
        {
            yield return new WaitForSeconds(1);
            GameManager.Instance.LoadLevel(DataManager.Instance.Level);
        }
    }

    public void SetTimer(int val)
    {
        _timerTxt.text = Math.Clamp(val, 0, 5).ToString();
    }

    private void SetStarMilestone()
    {
        
    }

    private void BeginDraw()
    {
        _timer.gameObject.SetActive(true);
        _tipsBtn.gameObject.SetActive(false);
    }

    private void EndDraw()
    {
        _anim.DOPlay();
        _timerImg.DOFillAmount(0, 6).SetEase(Ease.Linear).OnComplete(() => _tick.SetActive(true));
    }

    public void OnRestartBtnClick()
    {
        GameManager.Instance.LoadLevel(DataManager.Instance.GetLevel());
    }
}
