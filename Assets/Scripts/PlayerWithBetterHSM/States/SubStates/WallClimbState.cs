public class PlayerWallClimbState : PlayerTouchingWallState
{
    public PlayerWallClimbState(
        PlayerHSM currentContext,
        PlayerStateFactory states,
        PlayerData playerData,
        string animBoolName,
        bool isRootState = false
    )
        : base(currentContext, states, playerData, animBoolName, isRootState) { }

    public override void Exit()
    {
        base.Exit();
        context.SetVelocityY(0f);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        context.SetVelocityY(playerData.wallClimbVelocity * yInput);
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
