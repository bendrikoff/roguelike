using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private IInteractable _targetObject;

    public IInteractable TargetObject => _targetObject;
    
    private CircleCollider2D _collider;
    void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
    }
    

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<IInteractable>(out IInteractable component)&&_targetObject==null)
        {
            _targetObject = component;
            component.OpenInteract();
        }
        
        if (other.gameObject.TryGetComponent<IGetable>(out IGetable item))
        {
            item.Get();
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<IInteractable>(out IInteractable component))
        {
            if (component == _targetObject)
            {
                 _targetObject= null;
                 component.CloseInteract();
            }
        }
    }
}
