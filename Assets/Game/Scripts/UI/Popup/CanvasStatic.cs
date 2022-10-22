using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(20)]
public class CanvasStatic : Singleton<CanvasStatic>
{
    [SerializeField] private TMP_Text _coinTxt;
    [SerializeField] private Button _backBtn;
    [SerializeField] private Image _blackPanel;
    
    private void Awake()
    {
        DataManager.Instance.SetCoinEvent += UpdateCoinTxt;
        _backBtn.onClick.AddListener(OnBackBtnClick);
    }

    private void Start()
    {
        _coinTxt.text = DataManager.Instance.GetCoin().ToString();
    }

    private void UpdateCoinTxt(int totalCoin)
    {
        var a = int.Parse(_coinTxt.text);
        SonUtilities.TweenInt(a, totalCoin, Local);

        void Local(int a)
        {
            _coinTxt.text = a.ToString();
        }
    }

    private void OnBackBtnClick()
    {
        BackSystem.Instance.PopStack();
        if (GameManager.Instance.InRootScene)
        {
            return;
        }
        else
        {
            GameManager.Instance.LoadRootLevel();
        }
    }

    public void PlayBlackPanel()
    {
        _blackPanel.DOFade(1, 0.35f).SetEase(Ease.Linear).OnStart(() => _blackPanel.raycastTarget = true);
        _blackPanel.DOFade(0, 0.35f).SetEase(Ease.Flash).SetDelay(1)
            .OnComplete(() => _blackPanel.raycastTarget = false);
    }
}
