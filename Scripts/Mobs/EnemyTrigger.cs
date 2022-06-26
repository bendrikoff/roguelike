using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    
    
    private bool _playerInTrigger;

    public bool PlayerInTrigger => _playerInTrigger;
    
    private void Start()
    {
        _playerInTrigger = false;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInTrigger = false;
        }
    }
}
