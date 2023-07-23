using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(
        Player player,
        PlayerStateMachine stateMachine,
        PlayerData playerData,
        string animationBoolName
    )
        : base(player, stateMachine, playerData, animationBoolName) { }

    public override void Enter(object data = null)
    {
        base.Enter(data);
        player.SetVelocityY(-playerData.wallSlideVelocity);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (xInput * player.FacingDirection > 0)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        player.SetVelocityY(-playerData.wallSlideVelocity);
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}
