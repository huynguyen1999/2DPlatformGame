using UnityEngine;

public class PlayerAbilityState : PlayerBaseState
{
    protected bool isAbilityDone;

    public PlayerAbilityState(
        PlayerHSM currentContext,
        PlayerStateFactory states,
        PlayerData playerData,
        string animBoolName,
        bool isRootState = false
    )
        : base(currentContext, states, playerData, animBoolName, isRootState)
    {
        isAbilityDone = false;
    }

    public override void Enter(object data = null)
    {
        base.Enter(data);
        isAbilityDone = false;
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
        PlayerAbilityState subState = null;
        if (jumpInput)
        {
            context.InputHandler.UseJumpInput();
            if (
                context.previousState == states.TouchingWallState
                || context.previousState == states.TouchingLedgeState
            )
            {
                subState = states.WallJumpState;
            }
            else
            {
                subState = states.JumpState;
            }
        }
        SetSubState(subState);
        subState?.Enter();
    }

    public override void CheckSwitchStates()
    {
        if (currentSubState != null && !(currentSubState as PlayerAbilityState).isAbilityDone)
        {
            return;
        }
        PlayerBaseState newState = null;
        if (isGrounded && context.CurrentVelocity.y < 0.01f)
        {
            newState = states.GroundedState;
        }
        else
        {
            newState = states.InAirState;
        }
        SwitchState(newState);
    }
}
