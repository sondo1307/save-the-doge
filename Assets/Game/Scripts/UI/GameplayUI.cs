using System;
using System.Collections;
using System.Collections.Generic;
using Assets.ProgressBars.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : Singleton<GameplayUI>
{
    [Header("-----UI-----"), Space(20f)] 
    [SerializeField] private TMP_Text _timerTxt;
    [SerializeField] private GuiProgressBarUI _slider;
    [SerializeField] private Text _starTxt;
    [SerializeField] private GameObject _winUI;
    [SerializeField] private GameObject _loseUI;
    
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
                new Vector2(GameplayManager.Instance.StarMilestonePercentage[i] / _width, 0);
        }
        
        _oriAmount = GameplayManager.Instance.DrawAmount;
        GameplayManager.Win += WinUI;
        GameplayManager.Lose += LoseUI;
    }

    public void UpdateSlider()
    {
        _slider.Value = GameplayManager.Instance.DrawAmount / _oriAmount;
        _starTxt.text = GameplayManager.Instance.GetCurrentMilestone().ToString();
    }

    private void WinUI()
    {
        _winUI.gameObject.SetActive(true);
    }

    private void LoseUI()
    {
        _loseUI.gameObject.SetActive(true);        
    }

    public void SetTimer(int val)
    {
        _timerTxt.text = Math.Clamp(val, 0, 5).ToString();
    }

    private void SetStarMilestone()
    {
        
    }
}
