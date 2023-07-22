using System;
using System.Collections.Generic;
using UnityEngine;

public class E2_MeleeAttackState : MeleeAttackState
{
    private Enemy2 _enemy;

    public E2_MeleeAttackState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animName,
        D_MeleeAttackState stateData,
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

        if (_isTargetInMaxAggroRange)
        {
            _stateMachine.ChangeState(_enemy.TargetDetectedState);
        }
        else
        {
            _stateMachine.ChangeState(_enemy.LookForTargetState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
