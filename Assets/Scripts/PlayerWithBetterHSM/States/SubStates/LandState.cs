public class PlayerLandState : PlayerGroundedState
{
    public bool IsHardLand = false;

    public PlayerLandState(
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
        if (data != null)
        {
            IsHardLand = (bool)data;
        }
    }

    public override void InitializeSubState() { }

    public override void CheckSwitchStates()
    {
        PlayerBaseState newState = null;
        if (isAnimationFinished)
        {
            newState = states.IdleState;
        }
        else if (xInput != 0 && !IsHardLand)
        {
            newState = states.MoveState;
        }
        SwitchState(newState);
    }
}
