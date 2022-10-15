using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance;

    private bool firstTimeGet = true;

    public static bool instanceExists
    {
        get
        {
            return _instance != null;
        }
    }

    public static T GetInstance()
    {
        return _instance;
    }

    public static T Instance
    {
        get
        {
            // if null, finds an existing one
            if (_instance == null) _instance = (T)FindObjectOfType(typeof(T));

            // if still null, instantiates
            if (_instance == null)
            {
                Debug.Log(">> Instantiating Singleton: " + typeof(T).Name + "\n" + System.Environment.StackTrace);
                GameObject singleton = new GameObject();
                _instance = singleton.AddComponent<T>(); 
                singleton.name = "(singleton)" + typeof(T).ToString();
            }

            // if not null but inactive, finds an active one
            else if (!_instance.gameObject.activeInHierarchy)
            {
                Object[] allObjsOfType = FindObjectsOfType(typeof(T));
                if (allObjsOfType.Length > 1)
                {
                    GameObject[] a = (GameObject[])allObjsOfType;
                    for (int i = 0; i < a.Length; i++)
                    {
                        if (a[i].activeInHierarchy)
                        {
                            _instance = a[i].GetComponent<T>();
                            return _instance;
                        }
                    }
                }
            }

            return _instance;
        }
    }
    protected void SetInstance(T instance)
    {
        _instance = instance;
    }    
}