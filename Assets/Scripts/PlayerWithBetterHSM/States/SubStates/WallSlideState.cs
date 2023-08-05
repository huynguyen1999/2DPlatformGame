using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(
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
        context.SetVelocityY(-playerData.wallSlideVelocity);
    }

    public override void InitializeSubState() { }

    public override void CheckSwitchStates()
    {
        PlayerTouchingWallState newState = null;
        if (xInput * context.FacingDirection > 0)
        {
            newState = states.WallGrabState;
        }
        SwitchState(newState);
    }
}
