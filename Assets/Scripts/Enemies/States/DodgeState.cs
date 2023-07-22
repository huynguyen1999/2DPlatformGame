using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DodgeState : State
{
    protected D_DodgeState _stateData;

    public DodgeState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animName,
        D_DodgeState stateData
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
