using UnityEngine;

public class MoveState : State
{
    protected D_MoveState _stateData;
    protected bool _isDetectingWall;
    protected bool _isDetectingLedge;

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
        _entity.SetVelocity(_stateData.MovementSpeed);
        _isDetectingLedge = _entity.CheckLedge();
        _isDetectingWall = _entity.CheckWall();
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
        _entity.SetVelocity(_stateData.MovementSpeed);
        Debug.Log($"ledge check: {_isDetectingLedge}, wall check: {_isDetectingWall}");
    }
}
