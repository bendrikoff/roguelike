using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healther : MonoBehaviour
{
    [SerializeField] private Image[] _hearts;
    [SerializeField] private Sprite _fullyHearth;
    [SerializeField] private Sprite _emptyHearth;

    private bool isResistance;

    private AnimationHandler _animationHandler;
    
    private int _hp;

    private int _allHearths;

    private void Start()
    {
        _animationHandler = GetComponent<AnimationHandler>();

        
        _hp = 3;
        _allHearths = 3;
        ResetHp();
    }

    public void AddOneHearth()
    {
        if(_allHearths+1<=5)
        {
            _allHearths++;
            ResetHp();
        }
    }

    public void HealthOneHp()
    {
        if(_hp+1<=5)
        {
            _hp++;
            ResetHp();
        }
    }

    private void ResetHp()
    {
        for (int i = 0; i < _allHearths; i++)
        {
            _hearts[i].gameObject.SetActive(true);
            if (i < _hp)
            {
                _hearts[i].sprite = _fullyHearth;
            }
            else
            {
                _hearts[i].sprite = _emptyHearth;
            }
        }
    }

    public void GetDamage()
    {
        if(isResistance) return;
        StartCoroutine(Resistance());
        
        _animationHandler.GetDamageAnimation();
        if(_hp>0)
        {
            _hp--;
            ResetHp();
        }
        else
        {
            Death();
        }
    }

    private void Death()
    {
        Debug.Log("Ты падох");
    }

    private IEnumerator Resistance()
    {
        isResistance = true;
        yield return new WaitForSeconds(1);
        isResistance = false;

    }
}
