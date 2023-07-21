using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class LookForPlayerState : State
{
    protected D_LookForPlayerState _stateData;
    protected bool _turnImmediately;
    protected bool _isPlayerInMinAggroRange;
    protected float _lastTurnTime;
    protected int _amountOfTurnsDone;
    protected bool _isAllTurnsDone;

    public LookForPlayerState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animBoolName,
        D_LookForPlayerState stateData
    )
        : base(entity, stateMachine, animBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        _isPlayerInMinAggroRange = _entity.CheckPlayerInMinAggroRange();
        _entity.SetVelocity(0f);
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
        _isPlayerInMinAggroRange = _entity.CheckPlayerInMinAggroRange();
    }

    public void SetTurnImmediately(bool flip)
    {
        _turnImmediately = flip;
    }
}
