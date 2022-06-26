using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour,IInteractable
{

    [SerializeField] private Animator _bladeVfxAnimator;
    
    [SerializeField] private WeaponTrigger _trigger;

    [SerializeField] private int _damage;
    
    private Animator _animator;
    
    private GameObject _interactWindow;
    private GameObject _player;
    private WeaponHandler _weaponHandler;
    

    private void Start()
    {
        _interactWindow=GameObject.FindWithTag("Interact");
        
        _player=GameObject.FindWithTag("Player");
        _weaponHandler = _player.GetComponent<WeaponHandler>();
        _animator = GetComponent<Animator>();
        
        

    }

    public void Attack()
    {
        _animator.SetTrigger("Attack");
        _bladeVfxAnimator.SetTrigger("Attack");
        
        foreach (var enemy in _trigger.GetTargetList())
        {
            enemy.GetDamage(_damage);
            
            //Возможно заменить
            //enemy.GetComponent<Rigidbody2D>().AddForce(transform.position);
        }
    }



    public void GetWeapon()
    {
        _weaponHandler.SetWeapon(this);
        Destroy(gameObject);
    }

    public void Interact()
    {
        GetWeapon();
    }

    public void OpenInteract()
    {
        if (_interactWindow == null) return;
        
        var interactPos = new Vector2(transform.position.x, transform.position.y +1);
        _interactWindow.SetActive(true);
        _interactWindow.transform.position = interactPos;
    }
    public void CloseInteract()
    {
        if (_interactWindow == null) return;
        _interactWindow.SetActive(false);
    }
    
}
