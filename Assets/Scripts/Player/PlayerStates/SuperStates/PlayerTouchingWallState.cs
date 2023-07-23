using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    protected int xInput,
        yInput;
    private float previousGravityScale;
    private float lastWallTouchTime = Mathf.NegativeInfinity;

    public PlayerTouchingWallState(
        Player player,
        PlayerStateMachine stateMachine,
        PlayerData playerData,
        string animationBoolName
    )
        : base(player, stateMachine, playerData, animationBoolName) { }

    public override void Enter(object data = null)
    {
        if (Time.time < lastWallTouchTime + playerData.wallTouchCoolDown)
        {
            stateMachine.RevertState();
            return;
        }
        base.Enter(data);
        previousGravityScale = player.RB.gravityScale;
        player.RB.gravityScale = 0f;
    }

    public override void Exit()
    {
        base.Exit();
        player.RB.gravityScale = previousGravityScale;
        lastWallTouchTime = Time.time;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.NormalizedInputX;
        yInput = player.InputHandler.NormalizedInputY;
        
        if (Time.time > startTime + playerData.wallStickTime)
        {
            stateMachine.ChangeState(player.InAirState);
        }
        else if (isGrounded)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if (!isTouchingWall || xInput * player.FacingDirection < 0)
        {
            stateMachine.ChangeState(player.InAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}
