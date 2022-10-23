using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopUp : BasePopupUI
{
    [SerializeField] private Toggle _sound;
    [SerializeField] private Toggle _vibrate;

    protected override void Awake()
    {
        base.Awake();
        _sound.onValueChanged.AddListener(OnSoundBtnClick);
        _vibrate.onValueChanged.AddListener(OnVibrateBtnClick);
        _sound.isOn = DataManager.Instance.GetSoundData() == 1;
        _vibrate.isOn = DataManager.Instance.GetVibrateData() == 1;
    }

    public void OnSoundBtnClick(bool val)
    {
        DataManager.Instance.SaveSoundData(val);
    }

    public void OnVibrateBtnClick(bool val)
    {
        DataManager.Instance.SaveVibrateData(val);
    }
}
