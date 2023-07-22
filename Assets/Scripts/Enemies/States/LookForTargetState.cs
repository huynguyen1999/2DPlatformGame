using System;
using System.Collections.Generic;
using UnityEngine;

public class LookForTargetState : State
{
    protected D_LookForTargetState _stateData;
    protected bool _turnImmediately;
    protected bool _isTargetInMinAggroRange,
        _isTargetInMaxAggroRange;
    protected float _lastTurnTime;
    protected int _amountOfTurnsDone;
    protected bool _isAllTurnsDone;

    public LookForTargetState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animName,
        D_LookForTargetState stateData
    )
        : base(entity, stateMachine, animName)
    {
        _stateData = stateData;
    }

    public override void Enter(object data = null)
    {
        base.Enter(data);
        _isTargetInMinAggroRange = _entity.CheckTargetInMinAggroRange();
        _isTargetInMaxAggroRange = _entity.CheckTargetInMaxAggroRange();
        _entity.SetXVelocity(0f);
        _amountOfTurnsDone = 0;
        _isAllTurnsDone = false;
        _lastTurnTime = Time.time;
        _turnImmediately = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (_turnImmediately)
        {
            _entity.Flip();
            _lastTurnTime = Time.time;
            _amountOfTurnsDone++;
            _turnImmediately = false;
        }
        else if ((Time.time >= _lastTurnTime + _stateData.TurnTransitionTime) && !_isAllTurnsDone)
        {
            _turnImmediately = true;
        }

        if (_amountOfTurnsDone >= _stateData.AmountOfTurns)
        {
            _isAllTurnsDone = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        _isTargetInMinAggroRange = _entity.CheckTargetInMinAggroRange();
        _isTargetInMaxAggroRange = _entity.CheckTargetInMaxAggroRange();
    }

    public void SetTurnImmediately(bool flip)
    {
        _turnImmediately = flip;
    }
}
