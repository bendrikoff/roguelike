using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    private int _money;

    public void GetMoney(int money)
    {
        _money += money;
        _moneyText.text = _money.ToString();
    }

    public void PayMoney(int money)
    {
        if (_money - money > 0)
        {
            _money -= money;
            _moneyText.text = _money.ToString();

        }
        else
        {
            
        }
        
    }
}
