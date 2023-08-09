using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DodgeState : State
{
    protected D_DodgeState _stateData;
    protected bool _isTargetInMinAggroRange,
        _isTargetInMaxAggroRange,
        _isTargetInCloseRangeAction;
    protected bool _isDodgeOver;
    protected float _lastDodgeTime = -100f;

    public DodgeState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animName,
        D_DodgeState stateData
    )
        : base(entity, stateMachine, animName)
    {
        _stateData = stateData;
    }

    public override void Enter(object data = null)
    {
        _startTime = Time.time;
        _entity.Anim.SetBool(_animName, true);
        TriggerDodge();
        _isDodgeOver = false;
        _lastDodgeTime = Time.time;
        _isTargetInMinAggroRange = _entity.CheckTargetInMinAggroRange();
        _isTargetInMaxAggroRange = _entity.CheckTargetInMaxAggroRange();
        _isTargetInCloseRangeAction = _entity.CheckTargetInCloseRangeAction();
    }

    public override void Exit()
    {
        _entity.Anim.SetBool(_animName, false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= _startTime + _stateData.DodgeDuration)
        {
            _isDodgeOver = true;
        }
        _entity.Anim.SetFloat("VerticalVelocity", _core.Movement.CurrentVelocity.y);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        _isTargetInMinAggroRange = _entity.CheckTargetInMinAggroRange();
        _isTargetInMaxAggroRange = _entity.CheckTargetInMaxAggroRange();
        _isTargetInCloseRangeAction = _entity.CheckTargetInCloseRangeAction();
    }

    public void TriggerDodge()
    {
        _core.Movement.SetVelocity(
            new Vector2(
                _stateData.DodgeJumpForce.x * _core.Movement.FacingDirection * -1,
                _stateData.DodgeJumpForce.y
            )
        );
    }
}
