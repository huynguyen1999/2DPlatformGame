using System;
using System.Collections.Generic;
using UnityEngine;

public class E2_TargetDetectedState : TargetDetectedState
{
    private Enemy2 _enemy;

    public E2_TargetDetectedState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animName,
        D_TargetDetectedState stateData,
        Enemy2 enemy
    )
        : base(entity, stateMachine, animName, stateData)
    {
        _enemy = enemy;
    }

    public override void Enter(object data = null)
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
        if (_performCloseRangeAction)
        {
            _stateMachine.ChangeState(_enemy.DodgeState);
            return;
        }

        if (_performLongRangeAction)
        {
            _stateMachine.ChangeState(_enemy.RangedAttackState);
        }
        if (!_isTargetInMaxAggroRange)
        {
            _stateMachine.ChangeState(_enemy.LookForTargetState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
