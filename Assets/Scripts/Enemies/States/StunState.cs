using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StunState : State
{
    protected D_StunState _stateData;
    protected AttackDetails _attackDetails;
    protected bool _isNoLongerStun;
    protected float _lastStunTime = -100f;
    protected bool _isTargetInMinAggroRange = false,
        _performCloseRangeAction = false;

    public StunState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animName,
        D_StunState stateData
    )
        : base(entity, stateMachine, animName)
    {
        _stateData = stateData;
    }

    public override void Enter(object data = null)
    {
        base.Enter(data);
        if (data is AttackDetails)
        {
            _attackDetails = (AttackDetails)data;
        }
        _isNoLongerStun = false;
        _isTargetInMinAggroRange = _entity.CheckTargetInMinAggroRange();
        _performCloseRangeAction = _entity.CheckTargetInCloseRangeAction();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        float stunDuration =
            _attackDetails.StunDuration != 0f
                ? _attackDetails.StunDuration
                : _stateData.DefaultStunDuration;
        if (Time.time >= _startTime + stunDuration)
        {
            _isNoLongerStun = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        _isTargetInMinAggroRange = _entity.CheckTargetInMinAggroRange();
        _performCloseRangeAction = _entity.CheckTargetInCloseRangeAction();
    }
}
