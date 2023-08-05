using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public abstract class PlayerBaseState
{
    protected Player context;
    protected PlayerStateFactory states;
    private string animBoolName;
    protected PlayerData playerData;
    protected bool isRootState = false;
    protected PlayerBaseState currentSuperState,
        currentSubState,
        previousSubState;
    public bool isAnimationFinished;
    protected float startTime;

    protected int xInput,
        yInput;
    protected bool jumpInput,
        dashInput,
        rollInput;
    protected bool isGrounded,
        isTouchingWall,
        isTouchingLedge,
        isTouchingCeiling;
    protected bool[] attackInputs;

    public PlayerBaseState(
        Player currentContext,
        PlayerStateFactory states,
        PlayerData playerData,
        string animBoolName,
        bool isRootState = false
    )
    {
        this.context = currentContext;
        this.states = states;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
        this.isRootState = isRootState;
    }

    public virtual void Enter(object data = null)
    {
        CheckInput();
        DoPhysicsCheck();
        startTime = Time.time;
        isAnimationFinished = false;
        context.Anim.SetBool(animBoolName, true);
        InitializeSubState();
    }

    public virtual void Exit()
    {
        context.Anim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {
        CheckInput();
        CheckSwitchStates();
        isAnimationFinished = context.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1;
    }

    public void CheckInput()
    {
        xInput = context.InputHandler.NormalizedInputX;
        yInput = context.InputHandler.NormalizedInputY;
        jumpInput = context.InputHandler.JumpInput;
        dashInput = context.InputHandler.DashInput;
        rollInput = context.InputHandler.RollInput;
        attackInputs = context.InputHandler.AttackInputs?.Clone() as bool[];
    }

    public virtual void PhysicsUpdate()
    {
        DoPhysicsCheck();
    }

    public virtual void DoPhysicsCheck()
    {
        isGrounded = context.CheckIfGrounded();
        isTouchingWall = context.CheckIfTouchingWall();
        isTouchingLedge = context.CheckIfTouchingLedge() == false && isTouchingWall == true;
        isTouchingCeiling = context.CheckIfTouchingCeiling();
    }

    /// <summary>
    /// If the entered state is a super state, it will set
    /// one active sub state
    /// </summary>
    public abstract void InitializeSubState();

    /// <summary>
    /// Based on certain conditions, change to another state
    /// </summary>
    public abstract void CheckSwitchStates();

    public void LogicUpdateStates()
    {
        LogicUpdate();
        if (currentSubState != null)
        {
            currentSubState.LogicUpdate();
        }
    }

    public void PhysicsUpdateStates()
    {
        PhysicsUpdate();
        if (currentSubState != null)
        {
            currentSubState.PhysicsUpdate();
        }
    }

    public void ExitStates()
    {
        if (currentSubState != null)
        {
            currentSubState.Exit();
            currentSubState = null;
        }
        Exit();
    }

    protected void SwitchState(PlayerBaseState newState)
    {
        if (newState == null)
            return;
        ExitStates();
        if (isRootState)
        {
            // root scope state
            context.previousState = context.currentState;
            context.currentState = newState;
        }
        else if (currentSuperState != null)
        {
            currentSuperState.SetPreviousSubState(context.currentState);
            currentSuperState.SetSubState(newState);
        }
        newState.Enter();
    }

    protected void SetSuperState(PlayerBaseState newSuperState)
    {
        currentSuperState = newSuperState;
    }

    protected void SetSubState(PlayerBaseState newSubState)
    {
        currentSubState = newSubState;
        if (newSubState != null)
        {
            newSubState.SetSuperState(this);
        }
    }

    protected void SetPreviousSubState(PlayerBaseState newPreviousSubState)
    {
        previousSubState = newPreviousSubState;
    }
}
