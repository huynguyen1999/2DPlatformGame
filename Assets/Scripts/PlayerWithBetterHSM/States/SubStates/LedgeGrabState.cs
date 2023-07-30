public class PlayerLedgeGrabState : PlayerTouchingLedgeState
{
    public PlayerLedgeGrabState(
        PlayerHSM currentContext,
        PlayerStateFactory states,
        PlayerData playerData,
        string animBoolName,
        bool isRootState = false
    )
        : base(currentContext, states, playerData, animBoolName, isRootState) { }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        isAnimationFinished = context.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f;
    }

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
