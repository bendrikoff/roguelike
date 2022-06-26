using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private int _attackDelay;

    private GameObject _player;
    private Healther _playerHealther;
    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _playerHealther = _player.GetComponent<Healther>();
    }

    public void Attack()
    {
        _playerHealther.GetDamage();
    }
}
