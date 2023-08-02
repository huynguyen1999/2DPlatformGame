using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Weapon weapon;

    public PlayerAttackState(
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
        weapon.EnterWeapon();
    }

    public override void Exit()
    {
        base.Exit();
        weapon.ExitWeapon();
        context.SetVelocityX(0f);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        weapon.LogicUpdateWeapon();
        if (weapon.CanFlip())
        {
            context.CheckIfShouldFlip(xInput);
        }
        if (weapon.IsAnimationFinished())
        {
            isAbilityDone = true;
        }
    }

    public override void InitializeSubState() { }

    public override void CheckSwitchStates() { }

    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
        weapon.InitializeWeapon(this);
    }

    public void SetVelocity(float velocity)
    {
        context.SetVelocityX(velocity * context.FacingDirection);
    }
}
