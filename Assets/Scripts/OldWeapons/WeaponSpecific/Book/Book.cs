// using UnityEngine;

// public class WeaponBook : AggressiveWeapon
// {
//     public BookChargeState chargeState;
//     public BookHoldState holdState;
//     public BookThrowState throwState;
//     public BookCloseState closeState;
//     public override void InitializeWeapon(Player player, PlayerData playerData, PlayerAttackState playerAttackState)
//     {
//         base.InitializeWeapon(player, playerData, playerAttackState);
//         chargeState = new BookChargeState(this, weaponData, player, playerData, playerAttackState, "Charge");
//         holdState = new BookHoldState(this, weaponData, player, playerData, playerAttackState, "Hold");
//         throwState = new BookThrowState(this, weaponData, player, playerData, playerAttackState, "Throw");
//         closeState = new BookCloseState(this, weaponData, player, playerData, playerAttackState, "Close");
//     }
//     protected override void Start()
//     {
//         base.Start();
//     }
//     public override void UseWeapon()
//     {
//         base.UseWeapon();
//         currentState = chargeState;
//         currentState.Enter();
//     }
//     public override void Update()
//     {
//         base.Update();
//         player.Core.Movement.SetVelocityX(0f);
//     }
// }