using System;
using UnityEngine;

public class TipController : MonoBehaviour
{
    [SerializeField] private Vector3[] _pos;
    [SerializeField] private LineRenderer _line;
    
    private void OnValidate()
    {
        _pos = new Vector3[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            _pos[i] = transform.GetChild(i).position;
        }
    }

    private void Awake()
    {
        GameplayManager.Instance.EndDraw += EndTip;
        GameplayManager.Instance.ShowTip += ShowTip;
    }

    [ContextMenu("Show")]
    public void ShowTip()
    {
        _line.positionCount = _pos.Length;
        _line.SetPositions(_pos);
    }

    public void EndTip()
    {
        _line.enabled = false;
    }
}