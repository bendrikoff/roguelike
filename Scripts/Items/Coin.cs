using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Coin : MonoBehaviour,IGetable
{
    private MoneyManager _moneyManager;
    void Awake()
    {
        var player = GameObject.FindWithTag("Player");
        _moneyManager = player.GetComponent<MoneyManager>();
    }
    

    public void Get()
    {
        _moneyManager.GetMoney(1);
        Destroy(gameObject);
    }
}
