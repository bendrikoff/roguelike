using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Drop : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private int _minCoin;
    [SerializeField] private int _maxCoin;
    
    [SerializeField] private GameObject _moneyPref;



    public void Dropped(Vector3 position)
    {

        int countCoins = Random.Range(_minCoin, _maxCoin);
        for (int i = 0; i < countCoins; i++)
        {
            float xOffset = Random.Range(-1.5f, 1.5f);
            float yOffset = Random.Range(-1.5f, 1.5f);

            Vector2 newPos = new Vector2(position.x+xOffset,position.y+yOffset);

            var coin = Instantiate(_moneyPref,newPos,transform.rotation);
        }
    }
}
