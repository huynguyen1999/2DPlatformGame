using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Weapon weapon;
    private float previousGravityScale;
    private float lastAttackTime;

    public PlayerAttackState(
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
        core.Movement.SetVelocity(Vector2.zero);
        previousGravityScale = context.RB.gravityScale;
        context.RB.gravityScale = 1f;
        weapon.UseWeapon();
    }

    public override void Exit()
    {
        base.Exit();
        core.Movement.SetVelocity(Vector2.zero);
        context.RB.gravityScale = previousGravityScale;
        lastAttackTime = Time.time;
    }
    public override void InitializeSubState() { }

    public override void CheckSwitchStates()
    {
    }

    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
        weapon.InitializeWeapon(context, playerData, this);
    }

    public void CheckIfShouldFlip()
    {
        if (xInput != 0)
        {
            core.Movement.CheckIfShouldFlip(xInput);
        }
    }
}
