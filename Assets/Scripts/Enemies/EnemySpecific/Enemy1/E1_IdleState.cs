using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class E1_IdleState : IdleState
{
    private Enemy1 _enemy;

    public E1_IdleState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animBoolName,
        D_IdleState stateData,
        Enemy1 enemy
    )
        : base(entity, stateMachine, animBoolName, stateData)
    {
        _enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (_isPlayerInMinAggroRange)
        {
            _stateMachine.ChangeState(_enemy.PlayerDetectedState);
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
