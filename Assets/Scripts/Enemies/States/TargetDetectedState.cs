using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetectedState : State
{
    protected D_TargetDetectedState _stateData;

    protected bool _isTargetInMinAggroRange,
        _isTargetInMaxAggroRange,
        _isDetectingLedge,
        _isDetectingWall,
        _performLongRangeAction,
        _isTargetInCloseRangeAction,
        _performCloseRangeAction;

    public TargetDetectedState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animBoolName,
        D_TargetDetectedState stateData
    )
        : base(entity, stateMachine, animBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter(object data=null)
    {
        base.Enter(data);
        _entity.SetXVelocity(0f);
        _isTargetInMinAggroRange = _entity.CheckTargetInMinAggroRange();
        _isTargetInMaxAggroRange = _entity.CheckTargetInMaxAggroRange();
        _isDetectingLedge = _entity.CheckLedge();
        _isDetectingWall = _entity.CheckWall();
        _performLongRangeAction = false;
        _performCloseRangeAction = false;
        _isTargetInCloseRangeAction = _entity.CheckTargetInCloseRangeAction();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (
            _isTargetInCloseRangeAction && Time.time >= _startTime + _stateData.CloseRangeActionTime
        )
        {
            _performCloseRangeAction = true;
        }
        if (Time.time >= _startTime + _stateData.LongRangeActionTime)
        {
            _performLongRangeAction = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        _isTargetInMinAggroRange = _entity.CheckTargetInMinAggroRange();
        _isTargetInMaxAggroRange = _entity.CheckTargetInMaxAggroRange();
        _isTargetInCloseRangeAction = _entity.CheckTargetInCloseRangeAction();
    }
}
