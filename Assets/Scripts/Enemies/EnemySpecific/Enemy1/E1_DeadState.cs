using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class E1_DeadState : DeadState
{
    private Enemy1 _enemy;

    public E1_DeadState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animBoolName,
        D_DeadState stateData,
        Enemy1 enemy
    )
        : base(entity, stateMachine, animBoolName, stateData)
    {
        _enemy = enemy;
    }

    public override void Enter(object data = null)
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
