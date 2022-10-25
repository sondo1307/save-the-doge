using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinPopUp : BasePopupUI
{
    [SerializeField] private Button _doubledBtn;
    [SerializeField] private Button _claimBtn;
    [SerializeField] private Button _retry;

    [SerializeField] private GameObject[] _stars;
    [SerializeField] private TMP_Text _lvTxt;
    
    


    protected override void Awake()
    {
        base.Awake();
        _doubledBtn.onClick.AddListener(OnDoubleBtnClick);
        _claimBtn.onClick.AddListener(OnClaimBtnClick);
        _retry.onClick.AddListener(OnRestartBtnClick);
    }

    public override void OpenPopUp(bool overrideAnimation = false)
    {
        base.OpenPopUp(overrideAnimation);
        foreach (var star in _stars)
        {
            star.SetActive(false);
        }

        // Already Increase Level
        _lvTxt.text = $"Level {DataManager.Instance.Level - 1}";
        StartCoroutine(DelayStar());
        
        IEnumerator DelayStar()
        {
            yield return new WaitForSeconds(1);
            for (int i = 0; i < GameplayManager.Instance.FinishMilestone; i++)
            {
                _stars[i].gameObject.SetActive(true);
                _stars[i].transform.localScale = Vector3.zero;
                _stars[i].transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutQuad);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    private void OnDoubleBtnClick()
    {
        DataManager.Instance.ChangeCoin(40);
        StartCoroutine(Delay());
    }

    private void OnClaimBtnClick()
    {
        DataManager.Instance.ChangeCoin(20);
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        GameManager.Instance.LoadNextLevel();
    }
    
    private void OnRestartBtnClick()
    {
        // Already Save Next level
        GameManager.Instance.LoadLevel(DataManager.Instance.GetLevel() - 1);
    }
}
