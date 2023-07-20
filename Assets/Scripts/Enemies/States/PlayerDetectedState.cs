using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    protected D_PlayerDetectedState _stateData;
    protected bool _isPlayerInMinAggroRange,
    _isPlayerInMaxAggroRange;

    public PlayerDetectedState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animBoolName,
        D_PlayerDetectedState stateData
    )
        : base(entity, stateMachine, animBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        _entity.SetVelocity(0f);
        _isPlayerInMinAggroRange = _entity.CheckPlayerInMinAggroRange();
        _isPlayerInMaxAggroRange = _entity.CheckPlayerInMaxAggroRange();
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
        _isPlayerInMinAggroRange = _entity.CheckPlayerInMinAggroRange();
        _isPlayerInMaxAggroRange = _entity.CheckPlayerInMaxAggroRange();
    }
}
