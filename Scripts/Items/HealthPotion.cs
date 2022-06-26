using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour, IGetable
{
    private Healther _healther;
    private void Awake()
    {
        var player = GameObject.FindWithTag("Player");
        _healther = player.GetComponent<Healther>();
    }
    
    public void Get()
    {
        _healther.HealthOneHp();
        Destroy(gameObject);
    }
}
