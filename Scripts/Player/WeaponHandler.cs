using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{

    [SerializeField] private GameObject[] _weaponPrefs;
    
    [SerializeField] private Transform _weaponHolder;
    

    private GameObject _currentVeapon;
    public Weapon GetCurrentWeapon => _currentVeapon.GetComponent<Weapon>();

    private void Start()
    {
       //SetWeapon(_weaponPrefs[0].GetComponent<Weapon>());
    }

    public void SetWeapon(Weapon weapon)
    {
        Destroy(_currentVeapon);
        _currentVeapon = Instantiate(weapon.gameObject, _weaponHolder.position, _weaponHolder.transform.rotation, _weaponHolder);

    }
}
