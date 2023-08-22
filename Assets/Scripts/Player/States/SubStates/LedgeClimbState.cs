using UnityEngine;

public class PlayerLedgeClimbState : PlayerTouchingLedgeState
{
    public PlayerLedgeClimbState(
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
        stopPosition.Set(
            context.transform.position.x + (core.Movement.FacingDirection * playerData.stopOffset.x),
            context.transform.position.y + playerData.stopOffset.y
        );
    }

    public override void Exit()
    {
        base.Exit();
        context.transform.position = stopPosition;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void InitializeSubState() { }

    public override void CheckSwitchStates()
    {
        PlayerBaseState newState = null;
        SwitchState(newState);
    }
}
