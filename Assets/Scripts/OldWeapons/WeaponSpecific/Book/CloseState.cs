// using UnityEngine;
// public class BookCloseState : WeaponBaseState
// {
//     public BookCloseState(WeaponBook context, WeaponData weaponData, Player player, PlayerData playerData, PlayerAttackState playerAttackState, string animBoolName) : base(context, weaponData, player, playerData, playerAttackState, animBoolName)
//     {
//     }
//     public override void CheckSwitchStates()
//     {
//         if (IsAnimationFinished())
//         {
//             SwitchState((context as WeaponBook).emptyState);
//         }
//     }
// }