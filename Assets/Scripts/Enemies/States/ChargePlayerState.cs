using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChargePlayerState : State
{
    protected D_ChargePlayerState _stateData;
    protected bool _isChargeTimeOver = false;
    protected bool _isPlayerInMinAggroRange;
    protected bool _isDetectingWall;
    protected bool _isDetectingLedge;

    public ChargePlayerState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animBoolName,
        D_ChargePlayerState stateData
    )
        : base(entity, stateMachine, animBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        _isDetectingLedge = _entity.CheckLedge();
        _isDetectingWall = _entity.CheckWall();
        _isPlayerInMinAggroRange = _entity.CheckPlayerInMinAggroRange();
        _entity.SetVelocity(_stateData.ChargeSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= _startTime + _stateData.ChargeTime)
        {
            _isChargeTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        _isDetectingLedge = _entity.CheckLedge();
        _isDetectingWall = _entity.CheckWall();
        _isPlayerInMinAggroRange = _entity.CheckPlayerInMinAggroRange();
        _entity.SetVelocity(_stateData.ChargeSpeed);
    }
}
