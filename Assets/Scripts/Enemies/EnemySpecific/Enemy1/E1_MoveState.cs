using System;
using System.Collections.Generic;
using UnityEngine;

public class E1_MoveState : MoveState
{
    private Enemy1 _enemy;

    public E1_MoveState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animName,
        D_MoveState stateData,
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
        if (_isTargetInMinAggroRange)
        {
            _stateMachine.ChangeState(_enemy.TargetDetectedState);
        }
        if (!_isTouchingLedge || _isTouchingWall)
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
