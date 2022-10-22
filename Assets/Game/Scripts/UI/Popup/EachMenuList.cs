using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class EachMenuList : MonoBehaviour
    {
        [SerializeField] private GameObject[] _starOn;
        [SerializeField] private TMP_Text _levelTxt;
        

        private int _myIndex;
        
        public void Setup()
        {
            var index = transform.GetSiblingIndex();
            _myIndex = index;
            _levelTxt.text = $"Level {_myIndex + 1}";
            var a = DataManager.Instance.GetStar(index + 1);
            foreach (var item in _starOn)
            {
                item.SetActive(false);
            }
            
            for (int i = 0; i < a; i++)
            {
                _starOn[i].SetActive(true);
            }
        }

        public void OnClick()
        {
            GameManager.Instance.LoadLevel(_myIndex + 1);
        }
    }
}