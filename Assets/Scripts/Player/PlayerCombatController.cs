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

public class PlayerCombatController : MonoBehaviour, IDamageable
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
    private PlayerController _playerController;
    private PlayerStats _stats;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool(CombatAnimation.CanAttack.ToString(), CombatEnabled);
        _playerController = GetComponent<PlayerController>();
        _stats = GetComponent<PlayerStats>();
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
        if (!collision.isTrigger && collision.gameObject.CompareTag("Enemy"))
        {
            IDamageable enemy = collision.gameObject.GetComponentInParent<IDamageable>();
            float dotProduct = Vector2.Dot(collision.transform.right, transform.right);
            bool isFromBehind = dotProduct > 0f;
            AttackDetails attackDetails =
                new(
                    attackSourceTransform: transform,
                    damage: isFromBehind ? Attack1Damage * 2 : Attack1Damage
                );
            enemy.OnHit(attackDetails);
        }
    }

    public void OnHit(AttackDetails attackDetails)
    {
        if (_playerController.IsDashing)
            return;
        Debug.Log("transform right: " + attackDetails.AttackSourceTransform.transform.right);
        _stats.TakeDamage(attackDetails.Damage);
        _playerController.KnockBack(attackDetails.AttackSourceTransform.transform.right.x);
    }
}
