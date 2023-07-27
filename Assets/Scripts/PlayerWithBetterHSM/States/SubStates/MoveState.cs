public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(
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

        context.SetVelocityX(playerData.movementVelocity * xInput);
        context.CheckIfShouldFlip(xInput);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void InitializeSubState() { }

    public override void CheckSwitchStates()
    {
        PlayerBaseState newState = null;
        if (xInput == 0)
        {
            newState = states.IdleState;
        }

        SwitchState(newState);
    }
}
