using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HurtType
{
    Physic,
    Disappear,
}

public class Player : Singleton<Player>
{
    [SerializeField] private Collider2D _collider;
    [SerializeField] private CircleCollider2D _triggerBee;
    [SerializeField] private float _triggerRadius = 3;
    [SerializeField] private Rigidbody2D _rb;
    
    
    private void OnValidate()
    {
        _collider = GetComponent<Collider2D>();
        _triggerBee.radius = _triggerRadius;

    }

    private void Awake()
    {
        _triggerBee.enabled = false;
    }

    private void Start()
    {
        GameplayManager.Instance.Lose += StopPhysic;
        GameplayManager.Instance.Win += StopPhysic;
        GameplayManager.Instance.EndDraw += EndDraw;
    }
    

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(StaticValue.ENEMY_TAG))
        {
            col.GetComponent<Bee>().StartLoopForce();
        }
    }

    private void EndDraw()
    {
        _triggerBee.enabled = true;
    }

    private void StopPhysic()
    {
        _rb.bodyType = RigidbodyType2D.Static;
    }

    public void Hurt(HurtType type)
    {
        switch (type)
        {
            case HurtType.Physic:
                // Anim
                break;
            case HurtType.Disappear:
                // Particle
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, _triggerRadius / 2f);
    }
}
