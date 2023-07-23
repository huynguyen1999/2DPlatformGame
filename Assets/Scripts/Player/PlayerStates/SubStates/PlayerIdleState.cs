using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(
        Player player,
        PlayerStateMachine stateMachine,
        PlayerData playerData,
        string animationBoolName
    )
        : base(player, stateMachine, playerData, animationBoolName) { }

    public override void Enter(object data = null)
    {
        base.Enter(data);
        player.SetVelocityX(0f);
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
        if (xInput != 0)
        {
            stateMachine.ChangeState(player.MoveState);
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}
