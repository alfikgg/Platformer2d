using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PhysicsMovement))]
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PhysicsMovement _playerMoveScript;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private bool _isGrounded;

    private void Start()
    {
        _playerMoveScript = GetComponent<PhysicsMovement>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_playerMoveScript.targetVelocity.x > 0)
            _spriteRenderer.flipX = false;
        if (_playerMoveScript.targetVelocity.x < 0)
            _spriteRenderer.flipX = true;
        
        _animator.SetFloat("Speed", Mathf.Abs(_playerMoveScript.targetVelocity.x));

        _isGrounded = _playerMoveScript.grounded;

        if (!_isGrounded)
            _animator.SetBool("Jump", true);
        else
            _animator.SetBool("Jump", false);

    }
}
