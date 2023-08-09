using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerWallJumpState : PlayerAbilityState
{
    private float lastWallJump = Mathf.NegativeInfinity;

    public PlayerWallJumpState(
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
        isAbilityDone = true;
        core.Movement.Flip();
        context.RB.velocity = new Vector2(
            playerData.wallJumpForce.x * core.Movement.FacingDirection,
            playerData.wallJumpForce.y
        );
        states.InAirState.FreezeAction();
    }

    public override void Exit()
    {
        base.Exit();
        lastWallJump = Time.time;
    }

    public override void InitializeSubState() { }

    public override void CheckSwitchStates() { }

    public bool CanWallJump()
    {
        return Time.time > lastWallJump + playerData.wallJumpCoolDown;
    }
}
