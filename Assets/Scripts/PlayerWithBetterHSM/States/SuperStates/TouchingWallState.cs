using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerBaseState
{
    private float previousGravityScale;
    private float lastTouchWallTime = Mathf.NegativeInfinity;
    private bool canUseSkill;

    public PlayerTouchingWallState(
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
        if (isRootState)
        {
            previousGravityScale = context.RB.gravityScale;
            context.RB.gravityScale = 0f;
        }

        context.SetVelocityX(0f);
        context.StartCoroutine(ActivateSkillCoroutine());
    }

    private IEnumerator ActivateSkillCoroutine()
    {
        canUseSkill = false;
        yield return new WaitForSeconds(playerData.skillDelay);
        canUseSkill = true;
    }

    public override void Exit()
    {
        base.Exit();
        if (isRootState)
        {
            context.RB.gravityScale = previousGravityScale;
            lastTouchWallTime = Time.time;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoPhysicsCheck()
    {
        base.DoPhysicsCheck();
    }

    public override void InitializeSubState()
    {
        PlayerTouchingWallState subState = null;
        if (xInput * context.FacingDirection > 0)
        {
            subState = states.WallGrabState;
        }
        else
        {
            subState = states.WallSlideState;
        }
        SetSubState(subState);
        subState?.Enter();
    }

    public override void CheckSwitchStates()
    {
        PlayerBaseState newState = null;

        if (yInput < 0 || !isTouchingWall || Time.time > startTime + playerData.wallStickTime)
        {
            newState = states.InAirState;
        }
        else if (isGrounded && context.CurrentVelocity.y < 0.01f)
        {
            newState = states.GroundedState;
        }
        else if (canUseSkill && isTouchingWall)
        {
            if (jumpInput && states.WallJumpState.CanWallJump())
            {
                newState = states.AbilityState;
            }
            else if (dashInput && states.DashState.CanDash())
            {
                newState = states.AbilityState;
            }
        }
        SwitchState(newState);
    }

    public bool CanTouchWall()
    {
        return Time.time > lastTouchWallTime + playerData.wallTouchCoolDown;
    }
}
