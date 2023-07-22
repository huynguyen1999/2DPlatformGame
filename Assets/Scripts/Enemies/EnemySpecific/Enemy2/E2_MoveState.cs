using System;
using System.Collections.Generic;
using UnityEngine;

public class E2_MoveState : MoveState
{
    private Enemy2 _enemy;

    public E2_MoveState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animName,
        D_MoveState stateData,
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
        if (_isTargetInMinAggroRange)
        {
            _stateMachine.ChangeState(_enemy.TargetDetectedState);
        }
        if (!_isDetectingLedge || _isDetectingWall)
        {
            _enemy.IdleState.SetFlipAfterIdle(true);
            _stateMachine.ChangeState(_enemy.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
