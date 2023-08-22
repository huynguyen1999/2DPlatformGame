using UnityEngine;

public class PlayerCrouchIdleState : PlayerGroundedState
{
    public PlayerCrouchIdleState(
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
    }

    public override void InitializeSubState() { }

    public override void CheckSwitchStates()
    {
        PlayerBaseState newState = null;
        if (yInput >= 0 && !isTouchingCeiling)
        {
            newState = states.IdleState;
        }
        else if (xInput != 0)
        {
            newState = states.CrouchMoveState;
        }
        SwitchState(newState);
    }
}
