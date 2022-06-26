using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{

    private WeaponHandler _weaponHandler;

    private void Start()
    {
        _weaponHandler = GetComponent<WeaponHandler>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)&&_weaponHandler.GetCurrentWeapon!=null)
        {
            Attack(); 
        }
    }

    public void Attack()
    {
        _weaponHandler.GetCurrentWeapon.Attack();
    }
}
