using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class E1_TargetDetectedState : TargetDetectedState
{
    private Enemy1 _enemy;

    public E1_TargetDetectedState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animBoolName,
        D_TargetDetectedState stateData,
        Enemy1 enemy
    )
        : base(entity, stateMachine, animBoolName, stateData)
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
        if (_performCloseRangeAction)
        {
            _stateMachine.ChangeState(_enemy.MeleeAttackState);
            return;
        }

        if (_performLongRangeAction)
        {
            _stateMachine.ChangeState(_enemy.ChargeState);
        }
        else if (!_isTargetInMaxAggroRange)
        {
            _stateMachine.ChangeState(_enemy.LookForTargetState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
