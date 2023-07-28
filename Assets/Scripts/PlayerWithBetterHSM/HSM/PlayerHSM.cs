using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerHSM : MonoBehaviour
{
    #region Components
    public Animator Anim { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public BoxCollider2D PlayerCollier { get; private set; }
    #endregion

    #region Check Transforms
    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private Transform wallCheck;

    [SerializeField]
    private Transform ledgeCheck;
    #endregion

    #region State Variables
    public PlayerBaseState currentState;
    public PlayerBaseState previousState;
    public PlayerStateFactory states;
    #endregion

    #region Other Variables
    [SerializeField]
    private PlayerData playerData;
    private Vector2 workspace;
    public Vector2 CurrentVelocity
    {
        get
        {
            if (RB != null)
                return RB.velocity;
            return Vector2.zero;
        }
    }
    public int FacingDirection { get; private set; }
    #endregion
    //-----------------------------//
    #region Unity Callback Functions
    private void Awake()
    {
        states = new PlayerStateFactory(this, playerData);
        FacingDirection = 1;
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        PlayerCollier = GetComponent<BoxCollider2D>();
        InputHandler = GetComponent<PlayerInputHandler>();
        Initialize(states.GroundedState);
    }

    private void Update()
    {
        currentState.LogicUpdateStates();
    }

    private void FixedUpdate()
    {
        currentState.PhysicsUpdateStates();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, playerData.groundCheckRadius);
        Gizmos.DrawLine(
            wallCheck.position,
            wallCheck.position
                + new Vector3(playerData.wallCheckDistance * transform.right.x, 0f, 0f)
        );
        Gizmos.DrawLine(
            ledgeCheck.position,
            ledgeCheck.position
                + new Vector3(playerData.wallCheckDistance * transform.right.x, 0f, 0f)
        );
    }
    #endregion

    #region Set Methods

    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspace;
    }

    public void SetVelocity(Vector2 velocity2D)
    {
        RB.velocity = velocity2D;
    }
    #endregion

    #region Check Methods
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(
            groundCheck.position,
            playerData.groundCheckRadius,
            playerData.whatIsGround
        );
    }

    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(
            wallCheck.position,
            transform.right,
            playerData.wallCheckDistance,
            playerData.whatIsGround
        );
    }

    public bool CheckIfTouchingLedge()
    {
        return Physics2D.Raycast(
            ledgeCheck.position,
            transform.right,
            playerData.wallCheckDistance,
            playerData.whatIsGround
        );
    }

    #endregion

    // #region Coroutines
    // public void FreezeAction() => StartCoroutine(FreezeActionCoroutine());

    // private IEnumerator FreezeActionCoroutine()
    // {
    //     CanDoAction = false;
    //     yield return new WaitForSeconds(playerData.freezeMovementCoolDown);
    //     CanDoAction = true;
    // }
    // #endregion

    #region Other Methods
    private void Initialize(PlayerBaseState initState)
    {
        currentState = initState;
        currentState.Enter();
    }

    public Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(
            wallCheck.position,
            transform.right,
            playerData.wallCheckDistance,
            playerData.whatIsGround
        );
        float xDist = xHit.distance;
        workspace.Set(xDist * FacingDirection, 0f);
        RaycastHit2D yHit = Physics2D.Raycast(
            ledgeCheck.position + (Vector3)(workspace),
            -transform.up,
            ledgeCheck.position.y - wallCheck.position.y,
            playerData.whatIsGround
        );
        float yDist = yHit.distance;
        workspace.Set(
            wallCheck.position.x + (xDist * FacingDirection),
            ledgeCheck.position.y - yDist
        );
        return workspace;
    }

    public void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }
    #endregion
}
