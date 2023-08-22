public class PlayerLedgeGrabState : PlayerTouchingLedgeState
{
    public PlayerLedgeGrabState(
        Player currentContext,
        PlayerStateFactory states,
        PlayerData playerData,
        string animBoolName,
        bool isRootState = false
    )
        : base(currentContext, states, playerData, animBoolName, isRootState) { }

    public override void InitializeSubState() { }

    public override void CheckSwitchStates()
    {
        PlayerBaseState newState = null;
        if (isAnimationFinished)
        {
            newState = states.LedgeHoldState;
        }
        SwitchState(newState);
    }
}
