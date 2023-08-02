using UnityEngine;

public class PlayerTouchingLedgeState : PlayerBaseState
{
    protected Vector2 cornerPosition,
        startPosition,
        stopPosition;
    protected float previousGravityScale;
    protected float lastTouchLedgeTime;

    public PlayerTouchingLedgeState(
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
        if (!isRootState)
            return;
        context.SetVelocity(Vector2.zero);
        cornerPosition = context.DetermineCornerPosition();
        startPosition.Set(
            cornerPosition.x - (context.FacingDirection * playerData.startOffset.x),
            cornerPosition.y - playerData.startOffset.y
        );
        context.transform.position = startPosition;
        previousGravityScale = context.RB.gravityScale;
        context.RB.gravityScale = 0f;
    }

    public override void Exit()
    {
        base.Exit();
        if (isRootState)
        {
            context.RB.gravityScale = previousGravityScale;
            lastTouchLedgeTime = Time.time;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (isRootState)
        {
            context.SetVelocity(Vector2.zero);
            context.transform.position = startPosition;
        }
    }

    public override void DoPhysicsCheck()
    {
        base.DoPhysicsCheck();
    }

    public override void InitializeSubState()
    {
        PlayerTouchingLedgeState subState = states.LedgeGrabState;
        SetSubState(subState);
        subState.Enter();
    }

    public override void CheckSwitchStates()
    {
        PlayerBaseState newState = null;

        // done with climbing
        if (
            currentSubState == states.LedgeClimbState && currentSubState.isAnimationFinished == true
        )
        {
            newState = states.GroundedState;
        }
        else if (currentSubState != states.LedgeClimbState)
        {
            if (yInput < 0)
            {
                newState = states.InAirState;
            }
        }

        SwitchState(newState);
    }

    public bool CanTouchLedge()
    {
        bool isCoolDownOver = Time.time > lastTouchLedgeTime + playerData.ledgeTouchCoolDown;
        return isCoolDownOver;
    }
}
