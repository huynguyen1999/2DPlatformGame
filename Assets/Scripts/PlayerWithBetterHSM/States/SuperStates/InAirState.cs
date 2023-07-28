using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerInAirState : PlayerBaseState
{
    private bool coyoteTime = false;
    private bool canDoAction = true;

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
            context.StartCoroutine(DeactivateCoyoteTime());
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (canDoAction && (!isGrounded || Mathf.Abs(context.CurrentVelocity.y) > 0.01f))
        {
            context.CheckIfShouldFlip(xInput);
            context.SetVelocityX(playerData.movementVelocity * xInput);
        }
        if (isTouchingWall && !isTouchingLedge)
        {
            states.TouchingLedgeState.SetDetectedPosition(context.transform.position);
        }
        context.Anim.SetFloat("yVelocity", context.CurrentVelocity.y);
        context.Anim.SetFloat("xVelocity", Mathf.Abs(context.CurrentVelocity.x));
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (Mathf.Abs(context.CurrentVelocity.y) > playerData.maxFallingSpeed)
        {
            context.SetVelocityY(-playerData.maxFallingSpeed);
        }
    }

    public override void DoPhysicsCheck()
    {
        base.DoPhysicsCheck();
    }

    public override void InitializeSubState() { }

    public override void CheckSwitchStates()
    {
        PlayerBaseState newState = null;
        if (canDoAction && jumpInput && states.JumpState.CanJump())
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
        else if (isTouchingWall && !isTouchingLedge)
        {
            newState = states.TouchingLedgeState;
        }
        SwitchState(newState);
    }

    public void ActivateCoyoteTime() => coyoteTime = true;

    public IEnumerator DeactivateCoyoteTime()
    {
        yield return new WaitForSeconds(playerData.coyoteTime);
        states.JumpState.DecreaseAmountOfJumpsLeft();
        coyoteTime = false;
    }

    public void FreezeAction() => context.StartCoroutine(FreezeActionCoroutine());

    private IEnumerator FreezeActionCoroutine()
    {
        canDoAction = false;
        yield return new WaitForSeconds(playerData.freezeMovementCoolDown);
        canDoAction = true;
    }
}
