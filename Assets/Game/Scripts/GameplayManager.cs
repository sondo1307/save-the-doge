using System;
using System.Collections;
using System.Collections.Generic;
using Assets.ProgressBars.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(1)]
public class GameplayManager : Singleton<GameplayManager>
{
    public Action EndDraw;
    public Action BeginDraw;
    public Action Win;
    public Action Lose;
    public Action ShowTip;
    public float DrawAmount = 50f;
    public float[] StarMilestonePercentage;

    [SerializeField] private int _timer = 6;

    private float _oriDrawAmount;
    public int FinishMilestone;

    public int EndStatus = -1;

    private void Start()
    {
        EndDraw += StartCd;
        Win += StopCd;
        Lose += StopCd;
        Lose += LoseStatus;
        _oriDrawAmount = DrawAmount;
    }

    private void StartCd()
    {
        InvokeRepeating(nameof(MinusCd), 0, 1);
    }

    private void MinusCd()
    {
        GameplayUI.Instance.SetTimer(_timer);
        if (_timer <= 0)
        {
            Win?.Invoke();
            CancelInvoke();
            return;
        }
        _timer--;
    }

    private void StopCd()
    {
        CancelInvoke();
        DataManager.Instance.SaveStar(DataManager.Instance.Level, FinishMilestone);
    }

    private void LoseStatus()
    {
        EndStatus = 2;
    }

    public int GetCurrentMilestone()
    {
        var a = DrawAmount / _oriDrawAmount * 100;
        if (a >= StarMilestonePercentage[2])
        {
            return FinishMilestone = 3;
        }
        else if (a >= StarMilestonePercentage[1] && a < StarMilestonePercentage[2])
        {
            return FinishMilestone = 2;
        }

        return FinishMilestone = 1;
    }
}
