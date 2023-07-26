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
    public PlayerLedgeClimbState LedgeClimbState;

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
        GroundedState = new PlayerGroundedState(context, this, playerData, "Grounded", true);
        InAirState = new PlayerInAirState(context, this, playerData, "InAir", true);
        TouchingWallState = new PlayerTouchingWallState(
            context,
            this,
            playerData,
            "TouchingWall",
            true
        );
        LedgeClimbState = new PlayerLedgeClimbState(context, this, playerData, "LedgeClimb", true);
    }
}
