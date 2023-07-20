using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IdleState : State
{
    protected D_IdleState _stateData;
    protected bool _flipAfterIdle;
    protected bool _isIdleTimeOver;
    protected float _idleTime;

    public IdleState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animBoolName,
        D_IdleState stateData
    )
        : base(entity, stateMachine, animBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        _entity.SetVelocity(0f);
        _isIdleTimeOver = false;
        SetRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();

        if (_flipAfterIdle)
        {
            _entity.Flip();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= _startTime + _idleTime)
        {
            _isIdleTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetFlipAfterIdle(bool flip)
    {
        _flipAfterIdle = flip;
    }

    private void SetRandomIdleTime()
    {
        _idleTime = Random.Range(_stateData.MinIdleTime, _stateData.MaxIdleTime);
    }
}
