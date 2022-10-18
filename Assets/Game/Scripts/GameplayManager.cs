using System;
using System.Collections;
using System.Collections.Generic;
using Assets.ProgressBars.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : Singleton<GameplayManager>
{
    public static Action StartStage;
    public static Action Win;
    public static Action Lose;
    public float DrawAmount = 50f;
    public float[] StarMilestonePercentage;

    [SerializeField] private int _timer = 6;

    private float _oriDrawAmount;
    public int FinishMilestone;

    private void Start()
    {
        StartStage += StartCd;
        Win += StopCd;
        Lose += StopCd;
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
    }

    public int GetCurrentMilestone()
    {
        var a = DrawAmount / _oriDrawAmount;
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
