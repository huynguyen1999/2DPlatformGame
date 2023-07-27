using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data", order = 1)]
public class PlayerData : ScriptableObject
{
    [Header("HSM")]
    public float freezeMovementCoolDown = 0.5f;

    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public int amountOfJumps = 2;

    [Header("In Air State")]
    public float coyoteTime = 0.2f; // Time before player can jump again after leaving the ground

    [Header("Wall Touching State")]
    public float wallStickTime = 5f;
    public float wallTouchCoolDown = 0.3f;

    [Header("Wall Slide State")]
    public float wallSlideVelocity = 2f;

    [Header("Wall Climb State")]
    public float wallClimbVelocity = 5f;

    [Header("Wall Jump State")]
    public Vector2 wallJumpForce = new Vector2(20f, 10f);
    public float wallJumpCoolDown = 0.4f;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public float wallCheckDistance = 0.5f;
    public LayerMask whatIsGround;
}
