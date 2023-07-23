using UnityEngine;

public struct LandDTO
{
    public bool isHardLand;

    public LandDTO(bool isHardLand)
    {
        this.isHardLand = isHardLand;
    }
}

public class PlayerLandState : PlayerGroundedState
{
    private bool isHardLand;

    public PlayerLandState(
        Player player,
        PlayerStateMachine stateMachine,
        PlayerData playerData,
        string animationBoolName
    )
        : base(player, stateMachine, playerData, animationBoolName) { }

    public override void Enter(object data = null)
    {
        base.Enter(data);
        if (data is LandDTO)
        {
            isHardLand = ((LandDTO)data).isHardLand;
        }
        player.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
        isHardLand = false;
    }

    public override void LogicUpdate()
    {
        if (!isHardLand)
        {
            base.LogicUpdate();
            if (xInput != 0)
            {
                stateMachine.ChangeState(player.MoveState);
            }
            else if (isAnimationFinished)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            return;
        }
        // hard land => wait until animation finished
        if (!isAnimationFinished)
        {
            return;
        }
        stateMachine.ChangeState(player.IdleState);
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
