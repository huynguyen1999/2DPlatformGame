using UnityEngine;

public class MoveState : State
{
    protected D_MoveState _stateData;
    protected bool _isTargetInMinAggroRange;

    public MoveState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animName,
        D_MoveState stateData
    )
        : base(entity, stateMachine, animName)
    {
        _stateData = stateData;
    }

    public override void Enter(object data = null)
    {
        base.Enter(data);
        _isTargetInMinAggroRange = _entity.CheckTargetInMaxAggroRange();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        _isTargetInMinAggroRange = _entity.CheckTargetInMaxAggroRange();
        _core.Movement.SetVelocityX(_stateData.MovementSpeed * _core.Movement.FacingDirection);
    }
}
