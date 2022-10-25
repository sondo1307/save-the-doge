using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bee : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigid;
    [SerializeField] private float _force = 5f;
    [SerializeField] private float _delay = 5f;
    [SerializeField] private float _random = 0.25f;
    
    [Header("-------"), Space(10f)]
    [SerializeField] private Collider2D _colliderTrigger;
    [SerializeField] private Vector3 _des;
    [SerializeField] private float _speed = 0.25f;
    private bool _bool = true;
    
    
    private int _myIndex = 0;

    private void OnValidate()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _des = transform.GetChild(0).transform.position;
    }

    private void Awake()
    {
        GameplayManager.Instance.EndDraw += StartMove;
        GameplayManager.Instance.Win += StopMoving;
        GameplayManager.Instance.Lose += StopMoving;
        _myIndex = transform.GetSiblingIndex();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (_bool) return;
        if (Vector3.Distance(transform.position, _des) <= 1f)
        {
            StartLoopForce();
            return;
        }
        transform.position = Vector3.Lerp(transform.position, _des, _speed);
    }

    private void StartMove()
    {
        StartCoroutine(Delay());
        
        IEnumerator Delay()
        {
            yield return new WaitForSeconds(0.1f * _myIndex);
            _bool = false;
        }
    }

    private void StopMoving()
    {
        CancelInvoke();
        _rigid.velocity = Vector3.zero;
        _rigid.angularVelocity = 0;
    }
    
    public void AddForce()
    {
        _rigid.AddForce(
            ((Vector2) Player.Instance.transform.position + Random.insideUnitCircle * _random - (Vector2) transform.position)
            .normalized * _force, ForceMode2D.Impulse);
    }

    public void StartLoopForce()
    {
        if (_bool) return;
        _colliderTrigger.enabled = false;
        _bool = true;
        InvokeRepeating(nameof(AddForce), 0, _delay);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(StaticValue.LINE))
        {
            StartLoopForce();
        }
    }
}
