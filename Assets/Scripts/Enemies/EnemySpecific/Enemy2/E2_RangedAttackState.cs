using System;
using System.Collections.Generic;
using UnityEngine;

public class E2_RangedAttackState : RangedAttackState
{
    private Enemy2 _enemy;

    public E2_RangedAttackState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animName,
        D_RangedAttackState stateData,
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
        if (!_isAnimationFinished)
            return;

        if (!_isTargetInMaxAggroRange)
        {
            _stateMachine.ChangeState(_enemy.LookForTargetState);
        }
        else
        {
            _stateMachine.ChangeState(_enemy.TargetDetectedState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
