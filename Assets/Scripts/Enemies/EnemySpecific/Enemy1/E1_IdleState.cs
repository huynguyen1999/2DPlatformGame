using System;
using System.Collections.Generic;
using UnityEngine;

public class E1_IdleState : IdleState
{
    private Enemy1 _enemy;

    public E1_IdleState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animName,
        D_IdleState stateData,
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
        if (_isIdleTimeOver)
        {
            _stateMachine.ChangeState(_enemy.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
