using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public GameObject[] LevelPrefab;
    public GameObject LevelRoot;
    public GameObject CurrentLevelPrefab;

    public bool InRootScene = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadRootLevel();
    }

    public void LoadLevel(int level)
    {
        LoadLevelCommon(LocalFunc);
        InRootScene = false;

        void LocalFunc()
        {
            CurrentLevelPrefab = Instantiate(LevelPrefab[level]);
            BackSystem.Instance.PushStack(CurrentLevelPrefab);
            DataManager.Instance.Level = level;
        }
    }

    public void LoadNextLevel()
    {
        LoadLevelCommon(LocalFunc);
        InRootScene = false;

        void LocalFunc()
        {
            CurrentLevelPrefab = Instantiate(LevelPrefab[DataManager.Instance.GetLevel() + 1]);
            BackSystem.Instance.PushStack(CurrentLevelPrefab);
        }
    }

    public void LoadRootLevel()
    {
        LoadLevelCommon(LocalFunc);
        InRootScene = true;

        void LocalFunc()
        {
            CurrentLevelPrefab = Instantiate(LevelRoot);
            BackSystem.Instance.PushStack(CurrentLevelPrefab);
        }
    }

    private void LoadLevelCommon(Action callback)
    {
        CanvasStatic.Instance.PlayBlackPanel();
        BackSystem.Instance.PopStack();
        StartCoroutine(Delay());

        IEnumerator Delay()
        {
            yield return new WaitForSeconds(0.5f);
            callback?.Invoke();
        }
    }
}
