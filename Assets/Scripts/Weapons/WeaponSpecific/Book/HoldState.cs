using UnityEngine;
public class BookHoldState : WeaponBaseState
{
    public BookHoldState(WeaponBook context, WeaponData weaponData, Player player, PlayerData playerData, PlayerAttackState playerAttackState, string animBoolName) : base(context, weaponData, player, playerData, playerAttackState, animBoolName)
    {
    }
    public override void CheckSwitchStates()
    {
        if (Time.time >= startTime + weaponData.holdTime)
        {
            SwitchState((context as WeaponBook).throwState);
        }
    }
}