// using UnityEngine;

// public class WeaponSword : AggressiveWeapon
// {
//     private SwordAttackState attackState;
//     public override void InitializeWeapon(Player player, PlayerData playerData, PlayerAttackState playerAttackState)
//     {
//         base.InitializeWeapon(player, playerData, playerAttackState);
//         attackState = new SwordAttackState(this, weaponData, player, playerData, playerAttackState, "Attack");
//     }
//     public override void UseWeapon()
//     {
//         base.UseWeapon();
//         currentState = attackState;
//         currentState.Enter();
//     }
// }