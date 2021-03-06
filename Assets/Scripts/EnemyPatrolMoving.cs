﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class EnemyPatrolMoving : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _groundBottomDistanceCheck = 0.5f;
    [SerializeField] private LayerMask _layerMask;

    private bool _movingRight = false;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime);
        _animator.SetFloat("Speed", 1);

        RaycastHit2D groundCastDownInfo = Physics2D.Raycast(_groundCheck.position, Vector2.down, _groundBottomDistanceCheck, _layerMask);
        
        if (!groundCastDownInfo.collider)
        {
            if (_movingRight)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                _movingRight = false;
            }
            else if (!_movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                _movingRight = true;
            }
        }
    }
}
