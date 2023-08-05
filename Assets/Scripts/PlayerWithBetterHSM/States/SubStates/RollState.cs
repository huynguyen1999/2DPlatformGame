using UnityEngine;

public class PlayerRollState : PlayerAbilityState
{
    private float lastRollTime = Mathf.NegativeInfinity;

    public PlayerRollState(
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
        context.SetVelocityX(playerData.rollVelocity * context.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
        lastRollTime = Time.time;
        context.SetVelocityX(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        context.SetVelocityX(playerData.rollVelocity * context.FacingDirection);
        if (isAnimationFinished)
        {
            isAbilityDone = true;
        }
    }

    public override void InitializeSubState() { }

    public override void CheckSwitchStates() { }

    public bool CanRoll()
    {
        return Time.time > lastRollTime + playerData.rollCoolDown;
    }
}
