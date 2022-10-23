using System.Collections;
using Game;
using UnityEngine;

public class BackSystem : Singleton<BackSystem>
{
    public Stack Stack = new Stack();

    private float _time = 1;
    
    
    public void PushStack(object obj)
    {
        Stack.Push(obj);
    }

    public void PopStack()
    {
        if (Stack.Count == 0)
        {
            // Toast
            if (_time == 1)
            {
                StartTapExit();
            }
            else if (_time == 0)
            {
                // Exit
                Application.Quit();
            }
            return;
        }

        CancelTapExit();
        if (Stack.Peek() is GameObject)
        {
            Destroy(Stack.Pop() as GameObject);
        }
        else
        {
            var b = Stack.Pop() as BasePopupUI;
            b.ClosePopUpByBackSystem();
        }
    }

    public void OnlyPop()
    {
        if (Stack.Count == 0)
        {
            // Toast
            if (_time == 1)
            {
                StartTapExit();
            }
            else if (_time == 0)
            {
                // Exit
                Application.Quit();
            }
            return;
        }

        CancelTapExit();
        Stack.Pop();
    }

    private void StartTapExit()
    {
        _time = 0;
        Invoke(nameof(AAA), 1);
    }

    private void AAA()
    {
        _time = 1;
    }

    private void CancelTapExit()
    {
        CancelInvoke();
    }
}