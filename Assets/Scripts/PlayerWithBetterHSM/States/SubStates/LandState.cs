public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(
        PlayerHSM currentContext,
        PlayerStateFactory states,
        PlayerData playerData,
        string animBoolName,
        bool isRootState = false
    )
        : base(currentContext, states, playerData, animBoolName, isRootState) { }

    public override void Enter(object data = null)
    {
        base.Enter(data);
    }

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
            newState = states.IdleState;
        }
        else if (xInput != 0)
        {
            newState = states.MoveState;
        }
        SwitchState(newState);
    }
}
