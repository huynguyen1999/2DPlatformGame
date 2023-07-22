using System;
using System.Collections.Generic;
using UnityEngine;

public class E1_AttackState : AttackState
{
    private Enemy1 _enemy;

    public E1_AttackState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animName,
        D_AttackState stateData,
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
