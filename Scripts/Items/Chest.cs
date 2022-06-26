using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Chest : MonoBehaviour,IGetable
{
    [SerializeField] private GameObject _mimic;
    private Animator _animator;
    private Drop _drop;

    private bool isOpened;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _drop = GetComponent<Drop>();
    }

    public void Get()
    {
        if (isOpened) return;
        
        int random = Random.Range(0, 3);
        switch (random)
        {
            case 0:
                _animator.Play("OpenEmptyChest");
                break;
            case 1:
                _animator.Play("OpenFullChest");
                break;
            case 2:
                Instantiate(_mimic, transform.position,new Quaternion());
                Destroy(gameObject);
                break;
        }

        isOpened = true;

    }

    public void Drop()
    {
        _drop.Dropped(transform.position);
    }
}
