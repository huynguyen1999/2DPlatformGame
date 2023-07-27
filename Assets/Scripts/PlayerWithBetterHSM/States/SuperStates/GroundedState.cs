using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(
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
        if (isRootState)
        {
            states.JumpState.ResetAmountOfJumpsLeft();
        }
        context.SetVelocityX(0f);
        context.SetVelocityY(0f);
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
        if (context.previousState == states.InAirState)
        {
            subState = states.LandState;
        }
        else
        {
            subState = states.IdleState;
        }
        SetSubState(subState);
        subState.Enter();
    }

    public override void CheckSwitchStates()
    {
        // in the middle of land animation, can't do nothing
        // if (currentSubState == states.LandState && !currentSubState.isAnimationFinished)
        //     return;
        PlayerBaseState newState = null;
        if (jumpInput && states.JumpState.CanJump())
        {
            newState = states.AbilityState;
        }
        else if (!isGrounded)
        {
            newState = states.InAirState;
            states.InAirState.ActivateCoyoteTime();
        }
        SwitchState(newState);
    }
}
