public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(
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
        if (xInput != 0)
        {
            newState = states.MoveState;
        }
        else if (yInput < 0)
        {
            newState = states.CrouchIdleState;
        }

        SwitchState(newState);
    }
}
