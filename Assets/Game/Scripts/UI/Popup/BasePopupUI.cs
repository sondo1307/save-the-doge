using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class BasePopupUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _popUp;
        [SerializeField] private CanvasGroup _mainCg;
        [SerializeField] private Button _closeButton;
        
        
        protected virtual void Awake()
        {
            _closeButton?.onClick?.AddListener(ClosePopUpSelf);
        }
        
        public virtual void ClosePopUpByBackSystem()
        {
            _mainCg.HideCanvas();
        }

        public virtual void ClosePopUpSelf()
        {
            _mainCg.HideCanvas();
            BackSystem.Instance.OnlyPop();
        }

        public virtual void OpenPopUp(bool overrideAnimation = false)
        {
            
            if (overrideAnimation)
            {
                _popUp.localScale = Vector3.one * 1;
                _mainCg.ShowCanvas(0);
                return;
            }
            _mainCg.ShowCanvas();
            _popUp.localScale = Vector3.one * 0.8f;
            _popUp.DOPunchScale(Vector3.one * 0.2f, 0.25f).SetEase(Ease.Linear);
        }
    }
}