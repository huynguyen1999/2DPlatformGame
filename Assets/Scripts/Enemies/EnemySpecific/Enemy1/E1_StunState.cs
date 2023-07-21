using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class E1_StunState : StunState
{
    private Enemy1 _enemy;

    public E1_StunState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animBoolName,
        D_StunState stateData,
        Enemy1 enemy
    )
        : base(entity, stateMachine, animBoolName, stateData)
    {
        _enemy = enemy;
    }

    public override void Enter(object data = null)
    {
        if (Time.time <= _lastStunTime + _stateData.NormalAttackStunCoolDown)
        {
            _stateMachine.ChangeState(_stateMachine.PreviousState);
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
        if (_isNoLongerStun)
        {
            if (_performCloseRangeAction)
            {
                _stateMachine.ChangeState(_enemy.MeleeAttackState);
            }
            else if (_isTargetInMinAggroRange)
            {
                _stateMachine.ChangeState(_enemy.ChargeState);
            }
            else
            {
                _enemy.LookForTargetState.SetTurnImmediately(true);
                _stateMachine.ChangeState(_enemy.LookForTargetState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
