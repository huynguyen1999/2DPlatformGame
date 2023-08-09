using UnityEngine;

public class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(
        Player currentContext,
        PlayerStateFactory states,
        PlayerData playerData,
        string animBoolName,
        bool isRootState = false
    )
        : base(currentContext, states, playerData, animBoolName, isRootState) { }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        core.Movement.SetVelocityX(playerData.crouchVelocity * xInput);
        core.Movement.CheckIfShouldFlip(xInput);
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
