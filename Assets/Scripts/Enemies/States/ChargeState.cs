using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChargeState : State
{
    protected D_ChargeState _stateData;
    protected bool _isChargeTimeOver;
    protected bool _isTargetInMinAggroRange,
        _isTargetInMaxAggroRange;
    protected bool _isDetectingWall;
    protected bool _isDetectingLedge;
    protected bool _performCloseRangeAction;

    public ChargeState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animBoolName,
        D_ChargeState stateData
    )
        : base(entity, stateMachine, animBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter(object data = null)
    {
        base.Enter(data);
        _isChargeTimeOver = false;
        _entity.SetXVelocity(_stateData.ChargeSpeed);
        _isDetectingLedge = _entity.CheckLedge();
        _isDetectingWall = _entity.CheckWall();
        _isTargetInMinAggroRange = _entity.CheckTargetInMinAggroRange();
        _isTargetInMaxAggroRange = _entity.CheckTargetInMaxAggroRange();
        _performCloseRangeAction = _entity.CheckTargetInCloseRangeAction();
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
        _entity.SetXVelocity(_stateData.ChargeSpeed);
        _isDetectingLedge = _entity.CheckLedge();
        _isDetectingWall = _entity.CheckWall();
        _isTargetInMaxAggroRange = _entity.CheckTargetInMaxAggroRange();
        _isTargetInMinAggroRange = _entity.CheckTargetInMinAggroRange();
        _performCloseRangeAction = _entity.CheckTargetInCloseRangeAction();
    }
}
