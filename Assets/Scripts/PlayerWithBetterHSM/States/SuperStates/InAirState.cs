using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerInAirState : PlayerBaseState
{
    private bool coyoteTime = false;

    public PlayerInAirState(
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
        if (coyoteTime == true)
        {
            context.StartCoroutine(StartCoyoteTime());
        }
    }

    public override void Exit()
    {
        base.Exit();
        coyoteTime = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isGrounded || Mathf.Abs(context.CurrentVelocity.y) > 0.01f)
        {
            context.CheckIfShouldFlip(xInput);
            context.SetVelocityX(playerData.movementVelocity * xInput);
        }
        context.Anim.SetFloat("yVelocity", context.CurrentVelocity.y);
        context.Anim.SetFloat("xVelocity", Mathf.Abs(context.CurrentVelocity.x));
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoPhysicsCheck()
    {
        base.DoPhysicsCheck();
    }

    public override void InitializeSubState() { }

    public override void CheckSwitchStates()
    {
        PlayerBaseState newState = null;
        if (jumpInput && states.JumpState.CanJump())
        {
            newState = states.AbilityState;
        }
        else if (isGrounded && context.CurrentVelocity.y < 0.01f)
        {
            newState = states.GroundedState;
        }
        else if (
            isTouchingWall
            && xInput * context.FacingDirection > 0
            && context.CurrentVelocity.y < 0f
            && states.TouchingWallState.CanTouchWall()
        )
        {
            newState = states.TouchingWallState;
        }
        SwitchState(newState);
    }

    public void ActivateCoyoteTime()
    {
        coyoteTime = true;
    }

    public IEnumerator StartCoyoteTime()
    {
        yield return new WaitForSeconds(playerData.coyoteTime);
        states.JumpState.DecreaseAmountOfJumpsLeft();
    }
}
