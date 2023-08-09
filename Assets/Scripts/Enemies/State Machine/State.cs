using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected Core _core;
    protected FiniteStateMachine _stateMachine;
    protected Entity _entity;
    protected float _startTime;
    protected string _animName;

    protected bool _isTouchingLedge,
    _isTouchingWall;

    public State(Entity entity, FiniteStateMachine stateMachine, string animName)
    {
        _entity = entity;
        _stateMachine = stateMachine;
        _animName = animName;
        _core = entity.Core;
    }

    public virtual void Enter(object data = null)
    {
        _startTime = Time.time;
        _entity.Anim.Play(_animName);
        _isTouchingLedge = _core.CollisionDetection.CheckIfTouchingVerticalLedge();
        _isTouchingWall = _core.CollisionDetection.CheckIfTouchingWall();
    }

    public virtual void Exit()
    {
        _entity.Anim.StopPlayback();
    }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate()
    {
        _isTouchingLedge = _core.CollisionDetection.CheckIfTouchingVerticalLedge();
        _isTouchingWall = _core.CollisionDetection.CheckIfTouchingWall();
    }
}
