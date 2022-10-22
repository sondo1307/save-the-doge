using System;
using DG.Tweening;
using UnityEngine;

namespace Game
{
    public static class UIExtension
    {
        public static Tween ShowCanvas(this CanvasGroup cg, float time = 0.25f, Ease ease = Ease.Linear)
        {
            if (!cg.gameObject.activeInHierarchy)
            {
                cg.gameObject.SetActive(true);
                cg.HideCanvas(0);
            }
            cg.interactable = true;
            cg.blocksRaycasts = true;
            return cg.DOFade(1, 0.25f).SetEase(ease);
        }

        public static Tween HideCanvas(this CanvasGroup cg, float time = 0.25f, Ease ease = Ease.Linear)
        {
            cg.interactable = false;
            cg.blocksRaycasts = false;
            return cg.DOFade(0, 0.25f).SetEase(ease);
        }
    }

    public static class SonUtilities
    {
        public static Tween TweenInt(int start, int end, Action<int> onUpdate, float time = 0.35f, Ease ease = Ease.Linear)
        {
            var a = start;
            return DOTween.To(() => a, (x) => a = x, end, time).SetEase(ease).OnUpdate(() => onUpdate?.Invoke(a));
        }
    }
}