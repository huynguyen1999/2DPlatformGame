using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    public PlayerJumpState(
        Player player,
        PlayerStateMachine stateMachine,
        PlayerData playerData,
        string animationBoolName
    )
        : base(player, stateMachine, playerData, animationBoolName) { }

    public override void Enter(object data = null)
    {
        base.Enter(data);
        player.SetVelocityY(playerData.jumpVelocity);
        isAbilityDone = true;
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

    public override void DoChecks()
    {
        base.DoChecks();
    }
}
