using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    public E1_IdleState IdleState { get; private set; }
    public E1_MoveState MoveState { get; private set; }
    public E1_TargetDetectedState TargetDetectedState { get; private set; }
    public E1_ChargeState ChargeState { get; private set; }
    public E1_LookForTargetState LookForTargetState { get; private set; }
    public E1_AttackState AttackState { get; private set; }
    public E1_MeleeAttackState MeleeAttackState { get; private set; }
    public E1_StunState StunState { get; private set; }
    public E1_DeadState DeadState { get; private set; }

    [SerializeField]
    private D_IdleState _idleStateData;

    [SerializeField]
    private D_MoveState _moveStateData;

    [SerializeField]
    private D_TargetDetectedState _targetDetectedStateData;

    [SerializeField]
    private D_ChargeState _chargeStateData;

    [SerializeField]
    private D_LookForTargetState _lookForTargetStateData;

    [SerializeField]
    private D_AttackState _attackStateData;

    [SerializeField]
    private D_MeleeAttackState _meleeAttackStateData;

    [SerializeField]
    private D_StunState _stunStateData;

    [SerializeField]
    private D_DeadState _deadStateData;

    public override void Start()
    {
        base.Start();
        MoveState = new(this, StateMachine, "Enemy1_Move", _moveStateData, this);
        IdleState = new(this, StateMachine, "Enemy1_Idle", _idleStateData, this);
        TargetDetectedState = new(
            this,
            StateMachine,
            "Enemy1_TargetDetected",
            _targetDetectedStateData,
            this
        );
        ChargeState = new(this, StateMachine, "Enemy1_Charge", _chargeStateData, this);
        LookForTargetState = new(
            this,
            StateMachine,
            "Enemy1_LookForTarget",
            _lookForTargetStateData,
            this
        );
        AttackState = new(this, StateMachine, "Enemy1_Attack", _attackStateData, this);
        MeleeAttackState = new(this, StateMachine, "Enemy1_MeleeAttack", _meleeAttackStateData, this);
        StunState = new(this, StateMachine, "Enemy1_Stun", _stunStateData, this);
        DeadState = new(this, StateMachine, "Enemy1_Dead", _deadStateData, this);
        StateMachine.Initialize(MoveState);
    }
}
