using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _spriteGO;

    private EnemyAnimationHandler _animationHandler;
    
    private GameObject _player;
    private Rigidbody2D _rigidbody;
    
    private Vector3 _startSpriteScale;

    


    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _rigidbody = GetComponent<Rigidbody2D>();
        _startSpriteScale = _spriteGO.transform.localScale;

    }
    
    public void MoveToPlayer()
    {
        var dir = (_player.transform.position - transform.position).normalized;
        _rigidbody.AddForce(dir*_speed);
        _rigidbody.velocity=Vector2.zero;

        FlipSprite(dir);

    }

    private void FlipSprite(Vector3 dir)
    {
        if (dir.x <= 0)
        {
            _spriteGO.transform.localScale = new Vector3(-_startSpriteScale.x, _startSpriteScale.y);
        }
        else
        {
            _spriteGO.transform.localScale = new Vector3(_startSpriteScale.x, _startSpriteScale.y);

        }
    }
}
