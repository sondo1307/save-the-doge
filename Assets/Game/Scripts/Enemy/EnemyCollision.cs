using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField] private HurtType _hurtType;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag(StaticValue.PLAYER_TAG))
        {
            // Lose
            col.transform.GetComponent<Player>().Hurt(_hurtType);
            GameplayManager.Instance.Lose?.Invoke();
        }
    }
}
