using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    protected D_PlayerDetectedState _stateData;

    protected bool _isPlayerInMinAggroRange,
        _isPlayerInMaxAggroRange,
        _isDetectingLedge,
        _isDetectingWall,
        _isPlayerDetectedMinTimeOver;

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
        _isDetectingLedge = _entity.CheckLedge();
        _isDetectingWall = _entity.CheckWall();
        _isPlayerDetectedMinTimeOver = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= _startTime + _stateData.PlayerDetectedMinTime)
        {
            _isPlayerDetectedMinTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        _isPlayerInMinAggroRange = _entity.CheckPlayerInMinAggroRange();
        _isPlayerInMaxAggroRange = _entity.CheckPlayerInMaxAggroRange();
    }
}
