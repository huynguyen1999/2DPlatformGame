using UnityEngine;
public class PlayerJumpState : PlayerAbilityState
{
    private int amountOfJumpsLeft;

    public PlayerJumpState(
        Player currentContext,
        PlayerStateFactory states,
        PlayerData playerData,
        string animBoolName,
        bool isRootState = false
    )
        : base(currentContext, states, playerData, animBoolName, isRootState)
    {
        amountOfJumpsLeft = playerData.amountOfJumps;
    }

    public override void Enter(object data = null)
    {
        base.Enter(data);
        isAbilityDone = true;
        core.Movement.SetVelocityY(playerData.jumpVelocity);
        amountOfJumpsLeft--;
    }

    public override void InitializeSubState() { }

    public override void CheckSwitchStates() { }

    public bool CanJump()
    {
        return amountOfJumpsLeft > 0;
    }

    public void ResetAmountOfJumpsLeft()
    {
        amountOfJumpsLeft = playerData.amountOfJumps;
    }

    public void DecreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;
}
