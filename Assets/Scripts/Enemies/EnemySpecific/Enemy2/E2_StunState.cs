using System;
using System.Collections.Generic;
using UnityEngine;

public class E2_StunState : StunState
{
    private Enemy2 _enemy;

    public E2_StunState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animName,
        D_StunState stateData,
        Enemy2 enemy
    )
        : base(entity, stateMachine, animName, stateData)
    {
        _enemy = enemy;
    }

    public override void Enter(object data = null)
    {
        if (Time.time <= _lastStunTime + _stateData.NormalAttackStunCoolDown)
        {
            _stateMachine.RevertState();
            return;
        }
        base.Enter(data);
        _lastStunTime = Time.time;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!_isNoLongerStun)
            return;

        if (_performCloseRangeAction)
        {
            _stateMachine.ChangeState(_enemy.MeleeAttackState);
        }
        else if (_isTargetInMinAggroRange)
        {
            _stateMachine.ChangeState(_enemy.RangedAttackState);
        }
        else
        {
            _enemy.LookForTargetState.SetTurnImmediately(true);
            _stateMachine.ChangeState(_enemy.LookForTargetState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
