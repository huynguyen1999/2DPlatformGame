using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class E1_MoveState : MoveState
{
    private Enemy1 _enemy;

    public E1_MoveState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animBoolName,
        D_MoveState stateData,
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
