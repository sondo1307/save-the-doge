using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class EachMenuList : MonoBehaviour
    {
        [SerializeField] private GameObject[] _starOn;
        [SerializeField] private TMP_Text _levelTxt;

        [SerializeField] private GameObject _lock;
        [SerializeField] private GameObject _unlock;
        
        
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

            if (_myIndex == 0)
            {
                _lock.SetActive(false);
                _unlock.SetActive(true);
                for (int i = 0; i < a; i++)
                {
                    _starOn[i].SetActive(true);
                }

                return;
            }
            
            if (a == 0 && _myIndex != DataManager.Instance.GetLevel() - 1)
            {
                _lock.SetActive(true);
                _unlock.SetActive(false);
            }
            else
            {
                _lock.SetActive(false);
                _unlock.SetActive(true);
                for (int i = 0; i < a; i++)
                {
                    _starOn[i].SetActive(true);
                }
            }
        }

        public void OnClick()
        {
            BackSystem.Instance.PopStack();
            GameManager.Instance.LoadLevel(_myIndex + 1);
        }
    }
}