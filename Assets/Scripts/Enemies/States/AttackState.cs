using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackState : State
{
    protected D_AttackState _stateData;
    protected bool _isAnimationFinished;

    public AttackState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animName,
        D_AttackState stateData
    )
        : base(entity, stateMachine, animName)
    {
        _stateData = stateData;
    }

    public override void Enter(object data = null)
    {
        base.Enter(data);
        _isAnimationFinished = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (_entity.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            _isAnimationFinished = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public virtual void TriggerAttack() { }
}
