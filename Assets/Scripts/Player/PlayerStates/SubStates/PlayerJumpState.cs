// using UnityEngine;

// public class PlayerJumpState : PlayerAbilityState
// {
//     private int amountOfJumpsLeft;

//     public PlayerJumpState(
//         Player player,
//         PlayerStateMachine stateMachine,
//         PlayerData playerData,
//         string animationBoolName
//     )
//         : base(player, stateMachine, playerData, animationBoolName)
//     {
//         amountOfJumpsLeft = playerData.amountOfJumps;
//     }

//     public override void Enter(object data = null)
//     {
//         base.Enter(data);
//         isAbilityDone = true;
//         if (!CanJump())
//         {
//             stateMachine.RevertState();
//             return;
//         }
//         player.SetVelocityY(playerData.jumpVelocity);
//         amountOfJumpsLeft--;
//     }

//     public bool CanJump()
//     {
//         return amountOfJumpsLeft > 0;
//     }

//     public void ResetAmountOfJumpsLeft()
//     {
//         amountOfJumpsLeft = playerData.amountOfJumps;
//     }

//     public void DecreaseAmountOfJumpsLeft() => amountOfJumpsLeft--;
// }
