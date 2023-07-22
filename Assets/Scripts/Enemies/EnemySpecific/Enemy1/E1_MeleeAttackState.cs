using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class E1_MeleeAttackState : MeleeAttackState
{
    private Enemy1 _enemy;

    public E1_MeleeAttackState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animName,
        D_MeleeAttackState stateData,
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
        if (_isAnimationFinished)
        {
            if (_isTargetInMinAggroRange)
            {
                _stateMachine.ChangeState(_enemy.TargetDetectedState);
            }
            else
            {
                _stateMachine.ChangeState(_enemy.LookForTargetState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
