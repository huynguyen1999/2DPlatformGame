using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class E1_LookForTargetState : LookForTargetState
{
    private Enemy1 _enemy;

    public E1_LookForTargetState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animName,
        D_LookForTargetState stateData,
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
