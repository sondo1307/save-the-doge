using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

public class MenuListPopUp : BasePopupUI
{
    [SerializeField] private EachMenuList _each;
    [SerializeField] private Transform _parent;
    

    public override void OpenPopUp(bool overrideAnimation = false)
    {
        base.OpenPopUp(overrideAnimation);
        InstanceList();
    }

    private void InstanceList()
    {
        for (int i = 0; i < GameManager.Instance.LevelPrefab.Length; i++)
        {
            var a = Instantiate(_each, _parent);
            a.Setup();
            a.gameObject.SetActive(true);
        }
    }
}
