using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(
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
            states.JumpState.ResetAmountOfJumpsLeft();
        }
        context.SetVelocity(Vector2.zero);
    }

    public override void Exit()
    {
        base.Exit();
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
        PlayerGroundedState subState = null;
        bool isHardLand = false;
        if (context.previousState == states.InAirState)
        {
            subState = states.LandState;
            if (
                (context.previousState as PlayerInAirState).FallingDistance
                > playerData.HardLandDistance
            )
            {
                isHardLand = true;
            }
        }
        else if (isTouchingCeiling)
        {
            subState = states.CrouchIdleState;
        }
        else
        {
            subState = states.IdleState;
        }
        SetSubState(subState);
        subState.Enter(isHardLand);
    }

    public override void CheckSwitchStates()
    {
        // in the middle of land animation, can't do nothing
        if (
            currentSubState == states.LandState
            && (currentSubState as PlayerLandState).IsHardLand == true
            && !currentSubState.isAnimationFinished
        )
        {
            return;
        }

        PlayerBaseState newState = null;
        if (!isGrounded)
        {
            newState = states.InAirState;
            states.InAirState.ActivateCoyoteTime();
        }
        else if (!isTouchingCeiling)
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
}
