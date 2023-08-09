public class PlayerWallGrabState : PlayerTouchingWallState
{
    public PlayerWallGrabState(
        Player currentContext,
        PlayerStateFactory states,
        PlayerData playerData,
        string animBoolName,
        bool isRootState = false
    )
        : base(currentContext, states, playerData, animBoolName, isRootState) { }

    public override void Enter(object data = null)
    {
        base.Enter(data);
        core.Movement.SetVelocityY(0f);
    }

    public override void InitializeSubState() { }

    public override void CheckSwitchStates()
    {
        PlayerBaseState newState = null;
        if (yInput > 0)
        {
            newState = states.WallClimbState;
        }
        else if (xInput == 0)
        {
            newState = states.WallSlideState;
        }
        SwitchState(newState);
    }
}
