// using UnityEngine;

// public class PlayerGroundedState : PlayerState
// {
//     protected int xInput;
//     protected bool jumpInput;

//     public PlayerGroundedState(
//         Player player,
//         PlayerStateMachine stateMachine,
//         PlayerData playerData,
//         string animationBoolName
//     )
//         : base(player, stateMachine, playerData, animationBoolName) { }

//     public override void Enter(object data = null)
//     {
//         base.Enter(data);
//         player.JumpState.ResetAmountOfJumpsLeft();
//     }

//     public override void LogicUpdate()
//     {
//         base.LogicUpdate();

//         xInput = player.InputHandler.NormalizedInputX;
//         jumpInput = player.InputHandler.JumpInput;
//         if (jumpInput)
//         {
//             player.InputHandler.UseJumpInput(); // reset jump input
//             stateMachine.ChangeState(player.JumpState);
//         }
//         else if (!isGrounded)
//         {
//             player.InAirState.StartCoyoteTimer();
//             stateMachine.ChangeState(player.InAirState);
//         }
//     }
// }
