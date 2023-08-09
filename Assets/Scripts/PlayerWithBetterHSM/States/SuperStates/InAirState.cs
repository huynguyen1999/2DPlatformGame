using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerInAirState : PlayerBaseState
{
    private bool coyoteTime = false;
    private bool canDoAction = true;
    private float fallingDistance = 0f,
        midAirYPosition = 0f;
    public float FallingDistance
    {
        get { return fallingDistance; }
    }

    public PlayerInAirState(
        Player currentContext,
        PlayerStateFactory states,
        PlayerData playerData,
        string animBoolName,
        bool isRootState = false
    )
        : base(currentContext, states, playerData, animBoolName, isRootState)
    {

    }

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
        // calculate falling distance
        if (core.Movement.CurrentVelocity.y > -0.001f)
        {
            midAirYPosition = context.transform.position.y;
        }
        else if (core.Movement.CurrentVelocity.y <= -0.01f)
        {
            fallingDistance = Mathf.Abs(midAirYPosition - context.transform.position.y);
        }
        // check if player can move
        if (canDoAction && (!isGrounded || Mathf.Abs(core.Movement.CurrentVelocity.y) > 0.01f))
        {
            core.Movement.CheckIfShouldFlip(xInput);
            core.Movement.SetVelocityX(playerData.movementVelocity * xInput);
        }
        context.Anim.SetFloat("yVelocity", core.Movement.CurrentVelocity.y);
        context.Anim.SetFloat("xVelocity", Mathf.Abs(core.Movement.CurrentVelocity.x));
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (Mathf.Abs(core.Movement.CurrentVelocity.y) > playerData.maxFallingSpeed)
        {
            core.Movement.SetVelocityY(-playerData.maxFallingSpeed);
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

        if (isGrounded && core.Movement.CurrentVelocity.y < 0.01f)
        {
            newState = states.GroundedState;
        }
        if (newState == null && xInput * core.Movement.FacingDirection > 0 && core.Movement.CurrentVelocity.y < 0f)
        {
            if (isTouchingLedge && states.TouchingLedgeState.CanTouchLedge())
            {
                newState = states.TouchingLedgeState;
            }
            else if (isTouchingWall && states.TouchingWallState.CanTouchWall())
            {
                newState = states.TouchingWallState;
            }
        }
        if (newState == null && canDoAction)
        {
            if (jumpInput && states.JumpState.CanJump())
            {
                newState = states.AbilityState;
            }
            else if (dashInput && states.DashState.CanDash())
            {
                newState = states.AbilityState;
            }
            else if (rollInput && states.RollState.CanRoll())
            {
                newState = states.AbilityState;
            }
            else if (
                attackInputs != null
                && (
                    attackInputs[(int)CombatInputs.Primary]
                    || attackInputs[(int)CombatInputs.Secondary]
                )
            )
            {
                newState = states.AbilityState;
            }
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
