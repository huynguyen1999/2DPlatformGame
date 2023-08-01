using UnityEngine;

public class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(
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
        context.SetVelocityX(playerData.crouchVelocity * xInput);
        context.CheckIfShouldFlip(xInput);
    }

    public override void InitializeSubState() { }

    public override void CheckSwitchStates()
    {
        PlayerBaseState newState = null;
        if (xInput == 0f)
        {
            newState = states.CrouchIdleState;
        }
        else if (yInput >= 0 && !isTouchingCeiling)
        {
            newState = states.MoveState;
        }
        SwitchState(newState);
    }
}
