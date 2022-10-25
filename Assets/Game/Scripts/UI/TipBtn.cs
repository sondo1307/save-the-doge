using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
{
    public class TipBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            LinesDrawer.Instance.AllowDraw = false;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            LinesDrawer.Instance.AllowDraw = true;
        }
    }
}