using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] private Collider2D _collider;
    [SerializeField] private CircleCollider2D _triggerBee;
    [SerializeField] private float _triggerRadius = 3;
    
    
    
    private void OnValidate()
    {
        _collider = GetComponent<Collider2D>();
        _triggerBee.radius = _triggerRadius;

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag(StaticValue.ENEMY_TAG))
        {
            // Lose
            print("LOSE");
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(StaticValue.ENEMY_TAG))
        {
            col.GetComponent<Bee>().StartLoopForce();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, _triggerRadius / 2f);
    }
}
