using UnityEngine;

public class PlayerTouchingLedgeState : PlayerBaseState
{
    private Vector2 detectedPosition,
        cornerPosition,
        startPosition,
        stopPosition;
    private float previousGravityScale;

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
        context.transform.position = detectedPosition;
        context.SetVelocity(Vector2.zero);
        cornerPosition = context.DetermineCornerPosition();
        startPosition.Set(
            cornerPosition.x - (context.FacingDirection * playerData.startOffset.x),
            cornerPosition.y - playerData.startOffset.y
        );
        stopPosition.Set(
            cornerPosition.x + (context.FacingDirection * playerData.stopOffset.x),
            cornerPosition.y + playerData.stopOffset.y
        );
        context.transform.position = startPosition;
        previousGravityScale = context.RB.gravityScale;
        context.RB.gravityScale = 0f;
    }

    public override void Exit()
    {
        base.Exit();
        context.RB.gravityScale = previousGravityScale;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        context.SetVelocity(Vector2.zero);
        context.transform.position = startPosition;
    }

    public override void DoPhysicsCheck()
    {
        base.DoPhysicsCheck();
    }

    public override void InitializeSubState() { }

    public override void CheckSwitchStates() { }

    public void SetDetectedPosition(Vector2 position) => detectedPosition = position;
}
