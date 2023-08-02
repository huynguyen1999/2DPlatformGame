using UnityEngine;

public class PlayerStateFactory
{
    PlayerHSM context;
    PlayerData playerData;
    public PlayerIdleState IdleState;
    public PlayerJumpState JumpState;
    public PlayerLandState LandState;
    public PlayerMoveState MoveState;
    public PlayerWallClimbState WallClimbState;
    public PlayerWallGrabState WallGrabState;
    public PlayerWallSlideState WallSlideState;
    public PlayerAbilityState AbilityState;
    public PlayerGroundedState GroundedState;
    public PlayerInAirState InAirState;
    public PlayerTouchingWallState TouchingWallState;
    public PlayerWallJumpState WallJumpState;
    public PlayerTouchingLedgeState TouchingLedgeState;
    public PlayerLedgeClimbState LedgeClimbState;
    public PlayerLedgeGrabState LedgeGrabState;
    public PlayerLedgeHoldState LedgeHoldState;
    public PlayerDashState DashState;
    public PlayerCrouchIdleState CrouchIdleState;
    public PlayerCrouchMoveState CrouchMoveState;
    public PlayerRollState RollState;
    public PlayerAttackState PrimaryAttackState,
        SecondaryAttackState;

    public PlayerStateFactory(PlayerHSM currentContext, PlayerData playerData)
    {
        this.context = currentContext;
        this.playerData = playerData;
        IdleState = new PlayerIdleState(context, this, playerData, "Idle");
        JumpState = new PlayerJumpState(context, this, playerData, "Jump");
        LandState = new PlayerLandState(context, this, playerData, "Land");
        MoveState = new PlayerMoveState(context, this, playerData, "Move");
        WallClimbState = new PlayerWallClimbState(context, this, playerData, "WallClimb");
        WallGrabState = new PlayerWallGrabState(context, this, playerData, "WallGrab");
        WallSlideState = new PlayerWallSlideState(context, this, playerData, "WallSlide");
        AbilityState = new PlayerAbilityState(context, this, playerData, "Ability", true);
        WallJumpState = new PlayerWallJumpState(context, this, playerData, "WallJump", true);
        DashState = new PlayerDashState(context, this, playerData, "Dash");
        GroundedState = new PlayerGroundedState(context, this, playerData, "Grounded", true);
        InAirState = new PlayerInAirState(context, this, playerData, "InAir", true);
        TouchingWallState = new PlayerTouchingWallState(
            context,
            this,
            playerData,
            "TouchingWall",
            true
        );
        TouchingLedgeState = new PlayerTouchingLedgeState(
            context,
            this,
            playerData,
            "TouchingLedge",
            true
        );
        LedgeGrabState = new PlayerLedgeGrabState(context, this, playerData, "LedgeGrab");
        LedgeHoldState = new PlayerLedgeHoldState(context, this, playerData, "LedgeHold");
        LedgeClimbState = new PlayerLedgeClimbState(context, this, playerData, "LedgeClimb");
        CrouchIdleState = new PlayerCrouchIdleState(context, this, playerData, "CrouchIdle");
        CrouchMoveState = new PlayerCrouchMoveState(context, this, playerData, "CrouchMove");
        RollState = new PlayerRollState(context, this, playerData, "Roll");
        PrimaryAttackState = new PlayerAttackState(context, this, playerData, "Attack");
        SecondaryAttackState = new PlayerAttackState(context, this, playerData, "Attack");
    }
}
