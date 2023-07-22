using System;
using System.Collections.Generic;
using UnityEngine;

public class E2_DodgeState : DodgeState
{
    private Enemy2 _enemy;

    public E2_DodgeState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animName,
        D_DodgeState stateData,
        Enemy2 enemy
    )
        : base(entity, stateMachine, animName, stateData)
    {
        _enemy = enemy;
    }

    public override void Enter(object data = null)
    {
        if (Time.time < _lastDodgeTime + _stateData.DodgeCoolDown)
        {
            _stateMachine.ChangeState(_enemy.MeleeAttackState);
            return;
        }
        base.Enter(data);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!_isDodgeOver)
            return;

        if (!_isTargetInMaxAggroRange)
        {
            _stateMachine.ChangeState(_enemy.LookForTargetState);
        }
        else if (_isTargetInCloseRangeAction)
        {
            _stateMachine.ChangeState(_enemy.MeleeAttackState);
        }
        else
        {
            _stateMachine.ChangeState(_enemy.RangedAttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
