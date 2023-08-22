public class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(
        Player currentContext,
        PlayerStateFactory states,
        PlayerData playerData,
        string animBoolName,
        bool isRootState = false
    )
        : base(currentContext, states, playerData, animBoolName, isRootState) { }

    public override void Exit()
    {
        base.Exit();
        core.Movement.SetVelocityY(0f);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        core.Movement.SetVelocityY(playerData.wallClimbVelocity * yInput);
    }

    public override void InitializeSubState() { }

    public override void CheckSwitchStates()
    {
        if (yInput == 0)
        {
            SwitchState(states.WallGrabState);
        }
    }
}
