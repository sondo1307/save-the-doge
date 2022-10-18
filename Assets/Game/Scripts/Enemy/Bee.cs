using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bee : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigid;
    [SerializeField] private NavMeshAgent2D _agent;
    [SerializeField] private float _dis = 1f;
    [SerializeField] private float _force = 5f;
    [SerializeField] private float _delay = 5f;
    [SerializeField] private float _random = 0.25f;
    

    private void OnValidate()
    {
        _agent = GetComponent<NavMeshAgent2D>();
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent2D>();
        _agent.enabled = false;
        GameplayManager.StartStage += StartMove;
        GameplayManager.Win += StopMoving;
        GameplayManager.Lose += StopMoving;
    }

    private void StartMove()
    {
        _agent.enabled = true;
        _agent.SetDestination(Player.Instance.transform.position);
    }

    private void StopMoving()
    {
        _agent.enabled = false;
        _rigid.velocity = Vector3.zero;
        _rigid.angularVelocity = 0;
        CancelInvoke();
    }
    
    public void AddForce()
    {
        _rigid.AddForce(
            ((Vector2) Player.Instance.transform.position + Random.insideUnitCircle * _random - (Vector2) transform.position)
            .normalized * _force, ForceMode2D.Impulse);
    }

    public void StartLoopForce()
    {
        _agent.enabled = false;
        InvokeRepeating(nameof(AddForce), 0, _delay);
    }
}
