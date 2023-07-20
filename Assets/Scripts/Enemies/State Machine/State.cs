using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected FiniteStateMachine _stateMachine;
    protected Entity _entity;
    protected float _startTime;
    protected string _animBoolName;

    public State(Entity entity, FiniteStateMachine stateMachine, string animBoolName)
    {
        _entity = entity;
        _stateMachine = stateMachine;
        _animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        _startTime = Time.time;
        _entity.Anim.SetBool(_animBoolName, true);
    }

    public virtual void Exit()
    {
        _entity.Anim.SetBool(_animBoolName, false);
    }

    public virtual void LogicUpdate() { }

    public virtual void PhysicsUpdate() { }
}
