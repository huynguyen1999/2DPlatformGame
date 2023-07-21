using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    private D_MeleeAttackState _meleeAttackState;

    [SerializeField]
    private D_StunState _stunStateData;

    public override void Start()
    {
        base.Start();
        MoveState = new(this, StateMachine, "move", _moveStateData, this);
        IdleState = new(this, StateMachine, "idle", _idleStateData, this);
        TargetDetectedState = new(
            this,
            StateMachine,
            "targetDetected",
            _targetDetectedStateData,
            this
        );
        ChargeState = new(this, StateMachine, "charge", _chargeStateData, this);
        LookForTargetState = new(
            this,
            StateMachine,
            "lookForTarget",
            _lookForTargetStateData,
            this
        );
        AttackState = new(this, StateMachine, "attack", _attackStateData, this);
        MeleeAttackState = new(this, StateMachine, "meleeAttack", _meleeAttackState, this);
        StunState = new(this, StateMachine, "stun", _stunStateData, this);
        StateMachine.Initialize(IdleState);
    }

    public override void OnHit(AttackDetails attackDetails)
    {
        base.OnHit(attackDetails);
        if (_currentHealth <= 0)
        {
            //TODO: die
        }
        else
        {
            StateMachine.ChangeState(StunState, attackDetails);
        }
    }
}
