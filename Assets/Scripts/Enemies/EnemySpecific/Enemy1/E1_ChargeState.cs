using System;
using System.Collections.Generic;
using UnityEngine;

public class E1_ChargeState : ChargeState
{
    private Enemy1 _enemy;

    public E1_ChargeState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animName,
        D_ChargeState stateData,
        Enemy1 enemy
    )
        : base(entity, stateMachine, animName, stateData)
    {
        _enemy = enemy;
    }

    public override void Enter(object data=null)
    {
        base.Enter(data);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!_isTouchingLedge || _isTouchingWall)
        {
            _stateMachine.ChangeState(_enemy.LookForTargetState);
        }
        if (_isChargeTimeOver)
        {
            if (_isTargetInMinAggroRange)
                _stateMachine.ChangeState(_enemy.TargetDetectedState);
            else
                _stateMachine.ChangeState(_enemy.LookForTargetState);
        }
        if (_performCloseRangeAction)
        {
            _stateMachine.ChangeState(_enemy.MeleeAttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
