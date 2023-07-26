// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public enum PlayerAnimation
// {
//     Idle,
//     Move,
//     Jump,
//     Land,
//     InAir,
//     WallClimb,
//     WallGrab,
//     WallSlide
// }

// public class Player : MonoBehaviour
// {
//     #region Components
//     public Animator Anim { get; private set; }
//     public Rigidbody2D RB { get; private set; }
//     public PlayerInputHandler InputHandler { get; private set; }
//     public BoxCollider2D BoxCollider { get; private set; }
//     #endregion

//     #region State Variables
//     public PlayerStateMachine StateMachine { get; private set; }
//     public PlayerIdleState IdleState { get; private set; }
//     public PlayerMoveState MoveState { get; private set; }
//     public PlayerJumpState JumpState { get; private set; }
//     public PlayerLandState LandState { get; private set; }
//     public PlayerInAirState InAirState { get; private set; }
//     public PlayerWallClimbState WallClimbState { get; private set; }
//     public PlayerWallSlideState WallSlideState { get; private set; }
//     public PlayerWallGrabState WallGrabState { get; private set; }
//     #endregion

//     #region Other Variables
//     [SerializeField]
//     private PlayerData playerData;
//     private Vector2 workspace;
//     public Vector2 CurrentVelocity
//     {
//         get
//         {
//             if (RB != null)
//                 return RB.velocity;
//             return Vector2.zero;
//         }
//     }
//     public int FacingDirection { get; private set; }
//     #endregion

//     #region Check Transforms
//     [SerializeField]
//     private Transform groundCheck;

//     [SerializeField]
//     private Transform wallCheck;

//     [SerializeField]
//     private Transform ledgeCheck;
//     #endregion
//     //--------------------------//
//     #region Unity Callback Functions
//     private void Awake()
//     {
//         StateMachine = new PlayerStateMachine();
//         IdleState = new PlayerIdleState(
//             this,
//             StateMachine,
//             playerData,
//             PlayerAnimation.Idle.ToString()
//         );
//         MoveState = new PlayerMoveState(
//             this,
//             StateMachine,
//             playerData,
//             PlayerAnimation.Move.ToString()
//         );
//         JumpState = new PlayerJumpState(
//             this,
//             StateMachine,
//             playerData,
//             PlayerAnimation.Jump.ToString()
//         );
//         LandState = new PlayerLandState(
//             this,
//             StateMachine,
//             playerData,
//             PlayerAnimation.Land.ToString()
//         );
//         InAirState = new PlayerInAirState(
//             this,
//             StateMachine,
//             playerData,
//             PlayerAnimation.InAir.ToString()
//         );
//         WallClimbState = new PlayerWallClimbState(
//             this,
//             StateMachine,
//             playerData,
//             PlayerAnimation.WallClimb.ToString()
//         );
//         WallGrabState = new PlayerWallGrabState(
//             this,
//             StateMachine,
//             playerData,
//             PlayerAnimation.WallGrab.ToString()
//         );
//         WallSlideState = new PlayerWallSlideState(
//             this,
//             StateMachine,
//             playerData,
//             PlayerAnimation.WallSlide.ToString()
//         );
//         FacingDirection = 1;
//     }

//     private void Start()
//     {
//         Anim = GetComponent<Animator>();
//         RB = GetComponent<Rigidbody2D>();
//         BoxCollider = GetComponent<BoxCollider2D>();
//         InputHandler = GetComponent<PlayerInputHandler>();
//         StateMachine.Initialize(IdleState);
//     }

//     private void Update() => StateMachine.CurrentState.LogicUpdate();

//     private void FixedUpdate() => StateMachine.CurrentState.PhysicsUpdate();

//     private void OnDrawGizmos()
//     {
//         Gizmos.DrawWireSphere(groundCheck.position, playerData.groundCheckRadius);
//         Gizmos.DrawLine(
//             wallCheck.position,
//             wallCheck.position
//                 + new Vector3(playerData.wallCheckDistance * transform.right.x, 0f, 0f)
//         );
//     }
//     #endregion

//     #region Set Functions
//     public void SetVelocityX(float velocity)
//     {
//         workspace.Set(velocity, CurrentVelocity.y);
//         RB.velocity = workspace;
//     }

//     public void SetVelocityY(float velocity)
//     {
//         workspace.Set(CurrentVelocity.x, velocity);
//         RB.velocity = workspace;
//     }
//     #endregion

//     #region Check Functions
//     public void CheckIfShouldFlip(int xInput)
//     {
//         if (xInput != 0 && xInput != FacingDirection)
//         {
//             Flip();
//         }
//     }

//     public bool CheckIfGrounded()
//     {
//         return Physics2D.OverlapCircle(
//             groundCheck.position,
//             playerData.groundCheckRadius,
//             playerData.whatIsGround
//         );
//     }

//     public bool CheckIfTouchingWall()
//     {
//         return Physics2D.Raycast(
//             wallCheck.position,
//             transform.right,
//             playerData.wallCheckDistance,
//             playerData.whatIsGround
//         );
//     }

//     #endregion


//     #region Others (waiting to be named)
//     public void Flip()
//     {
//         FacingDirection *= -1;
//         transform.Rotate(0f, 180f, 0f);
//     }

//     private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

//     private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
//     #endregion
// }
