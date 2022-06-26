using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealther : MonoBehaviour
{
    [SerializeField] private int _hp;

    private EnemyAnimationHandler _animator;

    private Drop _drop;

    private void Start()
    {
        _drop = GetComponent<Drop>();
        _animator = GetComponent<EnemyAnimationHandler>();
    }

    public void GetDamage(int damage)
    {
        if (_hp - damage > 0)
        {
            _hp -= damage;
            _animator.GetDamageAnimation();
        }
        else
        {
            Death();
        }
    }

    private void Death()
    {
        _drop.Dropped(gameObject.transform.position);
        Destroy(this.gameObject);
    }
    
}
