using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Trigger _trigger;

    private WeaponHandler _weaponHandler;

    private void Start()
    {
        _weaponHandler = GetComponent<WeaponHandler>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_trigger.TargetObject !=  null)
            {
                _trigger.TargetObject.Interact();
                
            }
        }
    }
}
