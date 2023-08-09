using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChargeState : State
{
    protected D_ChargeState _stateData;
    protected bool _isChargeTimeOver;
    protected bool _isTargetInMinAggroRange,
        _isTargetInMaxAggroRange;
    protected bool _performCloseRangeAction;

    public ChargeState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animName,
        D_ChargeState stateData
    )
        : base(entity, stateMachine, animName)
    {
        _stateData = stateData;
    }

    public override void Enter(object data = null)
    {
        base.Enter(data);
        _isChargeTimeOver = false;
        _core.Movement.SetVelocityX(_stateData.ChargeSpeed * _core.Movement.FacingDirection);
        _isTargetInMinAggroRange = _entity.CheckTargetInMinAggroRange();
        _isTargetInMaxAggroRange = _entity.CheckTargetInMaxAggroRange();
        _performCloseRangeAction = _entity.CheckTargetInCloseRangeAction();
    }

    public override void Exit()
    {
        base.Exit();
        _core.Movement.SetVelocityX(0f);
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
        _core.Movement.SetVelocityX(_stateData.ChargeSpeed * _core.Movement.FacingDirection);
        _isTargetInMaxAggroRange = _entity.CheckTargetInMaxAggroRange();
        _isTargetInMinAggroRange = _entity.CheckTargetInMinAggroRange();
        _performCloseRangeAction = _entity.CheckTargetInCloseRangeAction();
    }
}
