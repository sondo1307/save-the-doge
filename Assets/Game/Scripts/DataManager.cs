using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(10)]
public class DataManager : Singleton<DataManager>
{
    public int Level;
    public int Coin;
    public int CurrentSkinIndex;

    public int MaxLevel = 20;

    public Action<int> SetCoinEvent;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Level = GetLevel();
    }

    public int GetLevel()
    {
        return PlayerPrefs.GetInt(StaticValue.LEVEL, 0);
    }

    public void SaveLevelNextLevel()
    {
        Level++;
        Level = Mathf.Clamp(Level, 0, MaxLevel);
        PlayerPrefs.SetInt(StaticValue.LEVEL, Level);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="val"></param>
    public void ChangeCoin(int val)
    {
        var a = PlayerPrefs.GetInt(StaticValue.COIN, 0);
        var b = a + val;
        PlayerPrefs.SetInt(StaticValue.COIN, b);
        SetCoinEvent?.Invoke(b);
        Coin = b;
    }

    public int GetCoin()
    {
        return Coin = PlayerPrefs.GetInt(StaticValue.COIN, 0);
    }

    public bool CheckEnoughCoin(int buyVal)
    {
        // buyVal > 0
        return PlayerPrefs.GetInt(StaticValue.COIN, 0) - buyVal >= 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int GetSkin()
    {
        return CurrentSkinIndex = PlayerPrefs.GetInt("SKIN", 0);
    }

    public void SaveSkin(int skinIndex)
    {
        PlayerPrefs.SetInt("SKIN", skinIndex);
        CurrentSkinIndex = skinIndex;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="level"></param>
    public void SaveStar(int level, int val)
    {
        var a = GetStar(level);
        if (val < a)
        {
            return;
        }
        PlayerPrefs.SetInt($"Star{level}", val);
    }

    public int GetStar(int level)
    {
        return PlayerPrefs.GetInt($"Star{level}", 0);
    }


    public void SaveSoundData(bool val)
    {
        PlayerPrefs.SetInt("Sound", val ? 1 : 0);
    }

    public void SaveVibrateData(bool val)
    {
        PlayerPrefs.SetInt("Vibrate", val ? 1 : 0);
    }

    public int GetSoundData()
    {
        return PlayerPrefs.GetInt("Sound", 1);
    }

    public int GetVibrateData()
    {
        return PlayerPrefs.GetInt("Vibrate", 1);
    }
}