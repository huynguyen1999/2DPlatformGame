using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class Enemy2 : Entity
{
    public E2_IdleState IdleState { get; private set; }
    public E2_MoveState MoveState { get; private set; }
    public E2_TargetDetectedState TargetDetectedState { get; private set; }
    public E2_LookForTargetState LookForTargetState { get; private set; }
    public E2_AttackState AttackState { get; private set; }
    public E2_MeleeAttackState MeleeAttackState { get; private set; }
    public E2_RangedAttackState RangedAttackState { get; private set; }
    public E2_StunState StunState { get; private set; }
    public E2_DeadState DeadState { get; private set; }
    public E2_DodgeState DodgeState { get; private set; }

    [SerializeField]
    private D_IdleState _idleStateData;

    [SerializeField]
    private D_MoveState _moveStateData;

    [SerializeField]
    private D_TargetDetectedState _targetDetectedStateData;

    [SerializeField]
    private D_LookForTargetState _lookForTargetStateData;

    [SerializeField]
    private D_AttackState _attackStateData;

    [SerializeField]
    private D_MeleeAttackState _meleeAttackStateData;

    [SerializeField]
    private D_RangedAttackState _rangedAttackStateData;

    [SerializeField]
    private D_StunState _stunStateData;

    [SerializeField]
    private D_DeadState _deadStateData;

    [SerializeField]
    private D_DodgeState _dodgeStateData;

    public override void Start()
    {
        base.Start();
        MoveState = new(this, StateMachine, "Enemy2_Move", _moveStateData, this);
        IdleState = new(this, StateMachine, "Enemy2_Idle", _idleStateData, this);
        TargetDetectedState = new(
            this,
            StateMachine,
            "Enemy2_TargetDetected",
            _targetDetectedStateData,
            this
        );
        LookForTargetState = new(
            this,
            StateMachine,
            "Enemy2_LookForTarget",
            _lookForTargetStateData,
            this
        );
        AttackState = new(this, StateMachine, "Enemy2_Attack", _attackStateData, this);
        MeleeAttackState = new(
            this,
            StateMachine,
            "Enemy2_MeleeAttack",
            _meleeAttackStateData,
            this
        );
        RangedAttackState = new(
            this,
            StateMachine,
            "Enemy2_RangedAttack",
            _rangedAttackStateData,
            this
        );
        DodgeState = new(this, StateMachine, "Enemy2_Dodge", _dodgeStateData, this);
        StunState = new(this, StateMachine, "Enemy2_Stun", _stunStateData, this);
        DeadState = new(this, StateMachine, "Enemy2_Dead", _deadStateData, this);
        StateMachine.Initialize(MoveState);
    }

    public override void OnHit(AttackDetails attackDetails)
    {
        base.OnHit(attackDetails);
        if (_currentHealth <= 0)
        {
            StateMachine.ChangeState(DeadState);
        }
        else
        {
            StateMachine.ChangeState(StunState, attackDetails);
        }
    }
}
