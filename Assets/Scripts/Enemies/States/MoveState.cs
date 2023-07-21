using UnityEngine;

public class MoveState : State
{
    protected D_MoveState _stateData;
    protected bool _isDetectingWall;
    protected bool _isDetectingLedge;
    protected bool _isPlayerInMinAggroRange;

    public MoveState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animBoolName,
        D_MoveState stateData
    )
        : base(entity, stateMachine, animBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        _isDetectingLedge = _entity.CheckLedge();
        _isDetectingWall = _entity.CheckWall();
        _isPlayerInMinAggroRange = _entity.CheckPlayerInMaxAggroRange();
        _entity.SetVelocity(_stateData.MovementSpeed);
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
        _isDetectingLedge = _entity.CheckLedge();
        _isDetectingWall = _entity.CheckWall();
        _isPlayerInMinAggroRange = _entity.CheckPlayerInMaxAggroRange();
        _entity.SetVelocity(_stateData.MovementSpeed);
    }
}
