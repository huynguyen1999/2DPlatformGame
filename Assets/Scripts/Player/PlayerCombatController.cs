using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatAnimation
{
    CanAttack,
    Attack1,
    IsAttacking,
    FirstAttack
}

public class PlayerCombatController : MonoBehaviour
{
    public bool CombatEnabled;
    public float InputTimer;
    public float Attack1Damage;
    public LayerMask WhatIsDamageable;

    private bool _gotInput;
    private bool _isAttacking;
    private bool _isFirstAttack;
    private bool _isAttack1;
    private float _lastInputTime = Mathf.NegativeInfinity;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool(CombatAnimation.CanAttack.ToString(), CombatEnabled);
    }

    private void Update()
    {
        CheckCombatInput();
        CheckAttacks();
        UpdateAnimations();
    }

    private void CheckCombatInput()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (CombatEnabled)
            {
                _gotInput = true;
                _lastInputTime = Time.time;
            }
        }
    }

    private void CheckAttacks()
    {
        if (_gotInput)
        {
            if (!_isAttacking)
            {
                _gotInput = false;
                _isAttacking = true;
                _isFirstAttack = !_isFirstAttack;
                _isAttack1 = true;
            }
        }
        if (Time.time >= _lastInputTime + InputTimer)
        {
            _gotInput = false;
        }
    }

    private void UpdateAnimations()
    {
        _animator.SetBool(CombatAnimation.FirstAttack.ToString(), _isFirstAttack);
        _animator.SetBool(CombatAnimation.IsAttacking.ToString(), _isAttacking);
        _animator.SetBool(CombatAnimation.Attack1.ToString(), _isAttack1);
    }

    private void FinishAttack1()
    {
        _isAttacking = false;
        _isFirstAttack = false;
        _isAttack1 = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IDamageable enemy = collision.gameObject.GetComponentInParent<IDamageable>();
            enemy.OnAttack(transform, Attack1Damage);
        }
    }
}
