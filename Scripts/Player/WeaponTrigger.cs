using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTrigger : MonoBehaviour
{
    
    private List<GameObject> _targetList = new List<GameObject>();

    
    public void OnTriggerEnter2D(Collider2D other)
    {
        _targetList.Add(other.gameObject);
    }
 
    public void OnTriggerExit2D(Collider2D other)
    {
        _targetList.Remove(other.gameObject);
    }

    public List<EnemyHealther> GetTargetList()
    {
        List<EnemyHealther> list = new List<EnemyHealther>();
        
        foreach (var target in _targetList)
        {
            if (target.TryGetComponent<EnemyHealther>(out var healther))
            { 
                list.Add(healther);
            }
        }
        
        
        
        return list;
    }
}
