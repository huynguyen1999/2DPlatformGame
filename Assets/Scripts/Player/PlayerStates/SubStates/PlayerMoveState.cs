// using UnityEngine;

// public class PlayerMoveState : PlayerGroundedState
// {
//     public PlayerMoveState(
//         Player player,
//         PlayerStateMachine stateMachine,
//         PlayerData playerData,
//         string animationBoolName
//     )
//         : base(player, stateMachine, playerData, animationBoolName) { }

//     public override void LogicUpdate()
//     {
//         base.LogicUpdate();
//         player.CheckIfShouldFlip(xInput);
//     }

//     public override void PhysicsUpdate()
//     {
//         base.PhysicsUpdate();
//         player.SetVelocityX(playerData.movementVelocity * xInput);
//         if (xInput == 0)
//         {
//             stateMachine.ChangeState(player.IdleState);
//         }
//     }
// }
