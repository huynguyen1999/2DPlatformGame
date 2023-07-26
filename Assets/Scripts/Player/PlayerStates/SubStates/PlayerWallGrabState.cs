// using UnityEngine;

// public class PlayerWallGrabState : PlayerTouchingWallState
// {
//     public PlayerWallGrabState(
//         Player player,
//         PlayerStateMachine stateMachine,
//         PlayerData playerData,
//         string animationBoolName
//     )
//         : base(player, stateMachine, playerData, animationBoolName) { }

//     public override void Enter(object data = null)
//     {
//         base.Enter(data);
//     }

//     public override void Exit()
//     {
//         base.Exit();
//     }

//     public override void LogicUpdate()
//     {
//         base.LogicUpdate();
//         player.SetVelocityX(0f);
//         player.SetVelocityY(0f);

//         if (yInput > 0)
//         {
//             stateMachine.ChangeState(player.WallClimbState);
//         }
//         else if (xInput == 0)
//         {
//             stateMachine.ChangeState(player.WallSlideState);
//         }
//     }

//     public override void PhysicsUpdate()
//     {
//         base.PhysicsUpdate();
//     }

//     public override void DoChecks()
//     {
//         base.DoChecks();
//     }
// }
