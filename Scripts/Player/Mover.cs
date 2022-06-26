using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private float _speed;
    private AnimationHandler _animationHandler;
    private Rigidbody2D _rigidbody;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animationHandler = GetComponent<AnimationHandler>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rigidbody.velocity=Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            _rigidbody.AddForce(Vector2.left*_speed);
            transform.localScale = new Vector3(-1, 1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _rigidbody.AddForce(Vector2.right*_speed);
            transform.localScale = new Vector3(1, 1);

        }
        if (Input.GetKey(KeyCode.W))
        {
            _rigidbody.AddForce(Vector2.up*_speed);

        }
        if (Input.GetKey(KeyCode.S))
        {
            _rigidbody.AddForce(Vector2.down*_speed);

        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            _animationHandler.StartMoveAnimation();
            _particle.Play();

        }
        else
        {
            _animationHandler.StopMoveAnimation();
            _particle.Stop();

        }
    }
}
