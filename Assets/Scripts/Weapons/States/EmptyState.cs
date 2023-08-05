using UnityEngine;
public class WeaponEmptyState : WeaponBaseState
{
    public WeaponEmptyState(Weapon context, WeaponData weaponData, Player player, PlayerData playerData, PlayerAttackState playerAttackState, string animBoolName) : base(context, weaponData, player, playerData, playerAttackState, animBoolName)
    {
    }
    public override void Enter(object data = null)
    {
        base.Enter(data);
        playerAttackState.IsAbilityDone = true;
        context.UnuseWeapon();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
    public override void CheckSwitchStates()
    {
    }
}