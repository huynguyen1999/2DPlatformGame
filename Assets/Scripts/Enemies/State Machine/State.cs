using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected FiniteStateMachine _stateMachine;
    protected Entity _entity;
    protected float _startTime;
    protected string _animName;

    public State(Entity entity, FiniteStateMachine stateMachine, string animName)
    {
        _entity = entity;
        _stateMachine = stateMachine;
        _animName = animName;
    }

    public virtual void Enter(object data = null)
    {
        _startTime = Time.time;
        _entity.Anim.Play(_animName);
    }

    public virtual void Exit() { }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate() { }
}
