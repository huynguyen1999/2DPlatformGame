using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RangedAttackState : State
{
    protected D_RangedAttackState _stateData;

    public RangedAttackState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animName,
        D_RangedAttackState stateData
    )
        : base(entity, stateMachine, animName)
    {
        _stateData = stateData;
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
