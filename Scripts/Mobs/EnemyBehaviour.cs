using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private EnemyTrigger _viewTrigger;
    [SerializeField] private EnemyTrigger _attackTrigger;
    
    private EnemyAnimationHandler _animationHandler;


    private Rigidbody2D _rigidbody;

    private EnemyAttacker _attacker;
    private EnemyMover _mover;
    void Start()
    {
        _mover = GetComponent<EnemyMover>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _animationHandler = GetComponent<EnemyAnimationHandler>();
        _attacker = GetComponent<EnemyAttacker>();

    }

    void FixedUpdate()
    {

        _rigidbody.velocity=Vector2.zero;
        
        if (!_viewTrigger.PlayerInTrigger)
        {
            _animationHandler.StopMoveAnimation();
            return;
        }
        
        
        if (_attackTrigger.PlayerInTrigger)
        {
            _attacker.Attack();
            _animationHandler.StopMoveAnimation();

        }
        else
        {
            _mover.MoveToPlayer();
            _animationHandler.StartMoveAnimation();
        }
        

 
        
    }
}
