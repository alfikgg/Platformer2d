using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PhysicsMovement))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Player))]

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PhysicsMovement _playerMoveScript;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Player _player;

    private bool _isGrounded;
    private bool _onLadder;
    private bool _isDead;

    private void Start()
    {
        _playerMoveScript = GetComponent<PhysicsMovement>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_playerMoveScript.targetVelocity.x > 0)
            _spriteRenderer.flipX = false;
        if (_playerMoveScript.targetVelocity.x < 0)
            _spriteRenderer.flipX = true;
        
        _animator.SetFloat("Speed", Mathf.Abs(_playerMoveScript.targetVelocity.x));

        _isGrounded = _playerMoveScript.grounded;
        _onLadder = _playerMoveScript.onLadder;
        _isDead = _player.isDead;

        if (!_isGrounded && !_onLadder)
            _animator.SetBool("Jump", true);
        else
            _animator.SetBool("Jump", false);

        if (_onLadder)
        {
            _animator.SetBool("OnLadder", true);
            if (Mathf.Abs(_playerMoveScript.targetVelocity.y) > 0.1)
                _animator.SetFloat("ClimbSpeed", 1);
            else
                _animator.SetFloat("ClimbSpeed", 0);
        }

        else if (!_onLadder)
            _animator.SetBool("OnLadder", false);

        if (_isDead)
            _animator.SetBool("IsDead", true);

    }
}
