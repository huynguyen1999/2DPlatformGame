using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class E1_LookForPlayerState : LookForPlayerState
{
    private Enemy1 _enemy;

    public E1_LookForPlayerState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animBoolName,
        D_LookForPlayerState stateData,
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
