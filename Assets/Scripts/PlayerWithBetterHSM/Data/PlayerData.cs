using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data", order = 1)]
public class PlayerData : ScriptableObject
{
    [Header("HSM")]
    public float freezeMovementCoolDown = 0.3f;

    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public int amountOfJumps = 2;

    [Header("Crouch States")]
    public float crouchVelocity = 5f;

    [Header("Roll State")]
    public float rollVelocity = 15f;

    [Header("Dash State")]
    public float directionSelectTimeScale = 0.1f;
    public float dashCoolDown = 1f;
    public float dashDistance = 10f;
    public float dashVelocity = 30f;
    public float afterImageDistance = 0.5f;
    public float directionSelectionTime = 0.5f;
    public float dashTime = 0.5f;

    [Header("In Air State")]
    public float coyoteTime = 0.2f; // Time before player can jump again after leaving the ground
    public float maxFallingSpeed = 20f;

    [Header("Touching Wall State")]
    public float wallStickTime = 2f;
    public float wallTouchCoolDown = 0.3f;
    public float skillDelay = 0.2f;

    [Header("Wall Slide State")]
    public float wallSlideVelocity = 2f;

    [Header("Wall Climb State")]
    public float wallClimbVelocity = 5f;

    [Header("Wall Jump State")]
    public Vector2 wallJumpForce = new Vector2(13f, 15f);
    public float wallJumpCoolDown = 0.2f;

    [Header("Touching Ledge State")]
    public float ledgeTouchCoolDown = 0.3f;

    [Header("Ledge Climb State")]
    public Vector2 startOffset = new Vector2(0.2f, 1f);
    public Vector2 stopOffset = new Vector2(0.5f, 2f);

    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public float wallCheckDistance = 0.4f;
    public LayerMask whatIsGround;
}
