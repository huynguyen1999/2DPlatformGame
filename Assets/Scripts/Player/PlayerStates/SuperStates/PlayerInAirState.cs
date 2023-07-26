// using UnityEngine;

// public class PlayerInAirState : PlayerState
// {
//     private int xInput;
//     private bool jumpInput;
//     private bool coyoteTime;

//     public PlayerInAirState(
//         Player player,
//         PlayerStateMachine stateMachine,
//         PlayerData playerData,
//         string animationBoolName
//     )
//         : base(player, stateMachine, playerData, animationBoolName)
//     {
//         coyoteTime = false;
//     }

//     public override void LogicUpdate()
//     {
//         base.LogicUpdate();
//         CheckCoyoteTime();
//         xInput = player.InputHandler.NormalizedInputX;
//         jumpInput = player.InputHandler.JumpInput;

//         if (isGrounded && player.CurrentVelocity.y < 0.01f)
//         {
//             stateMachine.ChangeState(player.LandState, new LandDTO(isHardLand: false));
//         }
//         else if (jumpInput)
//         {
//             player.InputHandler.UseJumpInput();
//             stateMachine.ChangeState(player.JumpState);
//         }
//         else if (
//             isTouchingWall && xInput * player.FacingDirection > 0 && player.CurrentVelocity.y <= 0f
//         )
//         {
//             stateMachine.ChangeState(player.WallGrabState);
//         }
//         else
//         {
//             player.CheckIfShouldFlip(xInput);
//             player.SetVelocityX(playerData.movementVelocity * xInput);
//         }
//         player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
//         player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));
//     }

//     public override void DoChecks()
//     {
//         base.DoChecks();
//         isGrounded = player.CheckIfGrounded();
//     }

//     private void CheckCoyoteTime()
//     {
//         if (coyoteTime && Time.time > startTime + playerData.coyoteTime)
//         {
//             coyoteTime = false;
//             player.JumpState.DecreaseAmountOfJumpsLeft();
//         }
//     }

//     public void StartCoyoteTimer() => coyoteTime = true;
// }
