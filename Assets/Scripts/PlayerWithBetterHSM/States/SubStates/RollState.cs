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
        core.Movement.SetVelocityX(playerData.rollVelocity * core.Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
        lastRollTime = Time.time;
        core.Movement.SetVelocityX(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        core.Movement.SetVelocityX(playerData.rollVelocity * core.Movement.FacingDirection);
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
