using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.UI;

public class Root : MonoBehaviour
{
    [SerializeField] private Button _startBtn;
    [SerializeField] private Button _menuListBtn;
    [SerializeField] private Button _skinBtn;
    [SerializeField] private Button _settingBtn;

    [SerializeField] private BasePopupUI _settingsPopup;
    [SerializeField] private BasePopupUI _menuPopup;
    [SerializeField] private BasePopupUI _skinPopup;
    

    private void Awake()
    {
        _startBtn.onClick.AddListener(OnStartBtnClick);
        _settingBtn.onClick.AddListener(OnSettingBtnClick);
        _menuListBtn.onClick.AddListener(OnMenuListBtnClick);
        _skinBtn.onClick.AddListener(OnShopBtnClick);
    }


    private void OnStartBtnClick()
    {
        GameManager.Instance.LoadLevel(DataManager.Instance.GetLevel());
    }

    private void OnSettingBtnClick()
    {
        var a = _settingsPopup as SettingPopUp;
        a.OpenPopUp(false);
        BackSystem.Instance.PushStack(a);
    }

    private void OnMenuListBtnClick()
    {
        var a = _menuPopup as MenuListPopUp;
        a.OpenPopUp(true);
        BackSystem.Instance.PushStack(a);
    }

    private void OnShopBtnClick()
    {
        var a = _skinPopup as ShopPopUp;
        a.OpenPopUp(true);
        BackSystem.Instance.PushStack(a);
    }
}
