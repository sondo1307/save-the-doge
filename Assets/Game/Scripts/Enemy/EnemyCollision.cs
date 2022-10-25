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
            switch (_hurtType)
            {
                case HurtType.Physic:
                    break;
                case HurtType.Disappear:
                    col.gameObject.SetActive(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            if (GameplayManager.Instance.EndStatus == 2)
            {
                return;
            }
            col.transform.GetComponent<Player>().Hurt(_hurtType);
            GameplayManager.Instance.Lose?.Invoke();
        }
    }
}
