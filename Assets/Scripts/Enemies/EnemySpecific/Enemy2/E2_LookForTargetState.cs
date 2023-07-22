using System;
using System.Collections.Generic;
using UnityEngine;

public class E2_LookForTargetState : LookForTargetState
{
    private Enemy2 _enemy;

    public E2_LookForTargetState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animName,
        D_LookForTargetState stateData,
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

        if (_isTargetInMaxAggroRange)
        {
            _stateMachine.ChangeState(_enemy.TargetDetectedState);
        }
        else if (_isAllTurnsDone)
        {
            _stateMachine.ChangeState(_enemy.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
