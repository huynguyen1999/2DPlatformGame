public class PlayerLedgeHoldState : PlayerTouchingLedgeState
{
    public PlayerLedgeHoldState(
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
        if (xInput * core.Movement.FacingDirection > 0)
        {
            newState = states.LedgeClimbState;
        }
        SwitchState(newState);
    }
}
