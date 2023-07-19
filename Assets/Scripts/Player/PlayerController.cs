using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Animation
{
    IsGrounded,
    IsWalking,
    IsWallSliding,
    IsClimbingLedge,
    IsDashing,
    VerticalVelocity
}

public delegate void Command();

public class PlayerController : MonoBehaviour
{
    public int AmountOfJumps;
    public float MovementSpeed;
    public float WallSlideSpeed;
    public float NormalJumpPower;
    public float WallJumpPower;
    public float BoxCastDistance;
    public float WallCheckDistance;
    public float WallSlideTimerSet;
    public float WallClingTimerSet;
    public float WallSlideTransitionTimerSet;
    public float TurnTimerSet;
    public float MaxFallingSpeed;
    public float DashTime;
    public float DashSpeed;
    public float DistanceBetweenAfterImages;
    public float DashCoolDown;
    public float KnockBackDuration;

    public Vector2 LedgeClimbOffset1;
    public Vector2 LedgeClimbOffset2;
    public Vector2 WallJumpDirection;
    public Vector2 KnockBackForce;
    public LayerMask WhatIsGround;
    public Transform LedgeCheck;
    public Transform WallCheck;

    private int _amountOfJumpsLeft;
    private int _facingDirection = 1;
    private bool _ledgeDetected = false;
    private bool _isWalking = false;
    private bool _isGrounded = false;
    private bool _isAgainstWall = false;
    private bool _isWallSliding = false;
    private bool _isTouchingLedge = false;
    private bool _canNormalJump = true;
    private bool _canWallJump = true;
    private bool _canMove = true;
    private bool _canFlip = true;
    private bool _canDash = true;
    private bool _canWallSlide = true;
    private bool _isClimbingLedge = false;
    private bool _isDashing = false;
    private bool _isPreviousWallSliding = false;
    private bool _isKnockBack = false;
    private float _movementDirection = 0f;
    private float _wallSlideTimer = 100f;
    private float _wallClingTimer;
    private float _wallSlideTransitionTimer;
    private float _turnTimer;
    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private List<Command> _jumpCommands = new();
    private BoxCollider2D _collider;
    private Vector2 _ledgeBottomPosition;
    private Vector2 _ledgePosition1;
    private Vector2 _ledgePosition2;
    private Vector2 _lastFrameDashPosition;

    public bool IsDashing
    {
        get { return _isDashing; }
    }

    public void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
        _amountOfJumpsLeft = AmountOfJumps;
        WallJumpDirection.Normalize();
    }

    public void Update()
    {
        CheckInput();
        CheckIfCanJump();
        UpdateAnimations();
        CheckLedgeClimb();
    }

    private void CheckInput()
    {
        _movementDirection = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            if (!_isWallSliding)
            {
                _jumpCommands.Add(NormalJump);
            }
            else
            {
                _jumpCommands.Add(WallJump);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            AttemptToDash();
        }
    }

    private void AttemptToDash()
    {
        if (!_canDash || _isClimbingLedge)
        {
            return;
        }
        _canDash = false;
        _isDashing = true;
        _canMove = false;
        _canFlip = false;
        _turnTimer = TurnTimerSet;
        PlayerAfterImagePool.Instance.GetFromPool();
        _lastFrameDashPosition = transform.position;
        StartCoroutine(StopDash());
        StartCoroutine(ResetDashCoolDown());
    }

    private IEnumerator StopDash()
    {
        yield return new WaitForSeconds(DashTime);
        _isDashing = false;
        _turnTimer = 0f;
    }

    private IEnumerator ResetDashCoolDown()
    {
        yield return new WaitForSeconds(DashCoolDown);
        _canDash = true;
    }

    private void CheckIfCanJump()
    {
        if (_isGrounded)
        {
            _amountOfJumpsLeft = AmountOfJumps;
        }

        if (_isWallSliding)
        {
            _canWallJump = true;
        }

        if (_amountOfJumpsLeft <= 0)
        {
            _canNormalJump = false;
        }
        else
        {
            _canNormalJump = true;
        }
    }

    private void CheckLedgeClimb()
    {
        if (_ledgeDetected && !_isClimbingLedge)
        {
            Flip(true);
            _isClimbingLedge = true;
            _isWallSliding = false;
            _canWallSlide = false;
            _canMove = false;
            _canFlip = false;
            _ledgePosition1 = new Vector2(
                _ledgeBottomPosition.x
                    + _facingDirection * (WallCheckDistance - LedgeClimbOffset1.x),
                _ledgeBottomPosition.y + LedgeClimbOffset1.y
            );
            _ledgePosition2 = new Vector2(
                _ledgeBottomPosition.x
                    + _facingDirection * (WallCheckDistance + LedgeClimbOffset2.x),
                _ledgePosition1.y + LedgeClimbOffset2.y
            );
        }
        if (_isClimbingLedge)
        {
            _turnTimer = TurnTimerSet;
            transform.position = _ledgePosition1;
        }
    }

    private void FinishLedgeClimb()
    {
        _ledgeDetected = false;
        _isClimbingLedge = false;
        _canWallSlide = true;
        _turnTimer = 0f;
        transform.position = _ledgePosition2;
    }

    private void NormalJump()
    {
        if (_canNormalJump)
        {
            _amountOfJumpsLeft--;
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, NormalJumpPower);
        }
    }

    private void WallJump()
    {
        // check if can wall jump
        if (_canWallJump)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0f);
            Vector2 jumpForceToAdd =
                new(
                    WallJumpPower * WallJumpDirection.x * _facingDirection,
                    WallJumpPower * WallJumpDirection.y
                );
            _rigidBody.AddForce(jumpForceToAdd, ForceMode2D.Impulse);
            _isWallSliding = false;
            // Flip();
            _canMove = false;
            _canFlip = false;
            _turnTimer = TurnTimerSet;
        }
    }

    private void UpdateAnimations()
    {
        _animator.SetBool(Animation.IsWalking.ToString(), _isWalking);
        _animator.SetBool(Animation.IsDashing.ToString(), _isDashing);
        _animator.SetBool(Animation.IsGrounded.ToString(), _isGrounded);
        _animator.SetBool(Animation.IsWallSliding.ToString(), _isWallSliding);
        _animator.SetBool(Animation.IsClimbingLedge.ToString(), _isClimbingLedge);
        _animator.SetFloat(Animation.VerticalVelocity.ToString(), _rigidBody.velocity.y);
    }

    public void FixedUpdate()
    {
        CheckCurrentState();
        CheckTimers();
        ApplyMovement();
    }

    private void CheckCurrentState()
    {
        _isGrounded =
            Physics2D
                .BoxCast(
                    _collider.bounds.center,
                    _collider.bounds.size,
                    0f,
                    -Vector2.up,
                    BoxCastDistance,
                    WhatIsGround
                )
                .collider != null;

        _isAgainstWall =
            Physics2D
                .Raycast(
                    WallCheck.transform.position,
                    -transform.right,
                    WallCheckDistance,
                    WhatIsGround
                )
                .collider != null
            || Physics2D
                .Raycast(
                    WallCheck.transform.position,
                    transform.right,
                    WallCheckDistance,
                    WhatIsGround
                )
                .collider != null;

        _isTouchingLedge =
            _isAgainstWall
            && !_isWallSliding
            && Physics2D
                .Raycast(
                    LedgeCheck.position,
                    transform.right,
                    _collider.bounds.extents.x + BoxCastDistance,
                    WhatIsGround
                )
                .collider == null;

        if (_isTouchingLedge)
        {
            _ledgeDetected = true;
            _ledgeBottomPosition = _collider.bounds.center;
        }

        _isWalking = !_isDashing && _isGrounded && !Mathf.Approximately(_movementDirection, 0f);

        _isWallSliding =
            // _canWallSlide &&
            !_isDashing && !_isClimbingLedge && !_isGrounded && _isAgainstWall;

        if (_isWallSliding && !_isPreviousWallSliding)
        {
            _isWallSliding = _isWallSliding && (_movementDirection * _facingDirection > 0f);
        }

        if (_isWallSliding && _wallSlideTimer <= 0f)
        {
            // _isWallSliding = false;
            // _canWallSlide = false;
            _wallSlideTransitionTimer = WallSlideTransitionTimerSet;
        }

        if (_isWallSliding == true && _isWallSliding != _isPreviousWallSliding)
        {
            Flip(true);
        }

        _isPreviousWallSliding = _isWallSliding;

        // after
        _rigidBody.gravityScale = (_isWallSliding || _isClimbingLedge || _isDashing) ? 0f : 4f;
    }

    private void CheckTimers()
    {
        if (_isWallSliding)
        {
            if (_wallSlideTimer > 0f)
            {
                _wallSlideTimer -= Time.deltaTime;
            }

            if (_wallClingTimer > 0f)
            {
                _wallClingTimer -= Time.deltaTime;
            }
        }

        if (_wallSlideTransitionTimer > 0f)
        {
            _wallSlideTransitionTimer -= Time.deltaTime;
        }
        else
        {
            _canWallSlide = true;
        }

        if (_turnTimer > 0f)
        {
            _turnTimer -= Time.deltaTime;
        }
        else
        {
            _canMove = true;
            _canFlip = true;
        }
    }

    private void ApplyMovement()
    {
        if (_isKnockBack || _isClimbingLedge)
            return;
        // perform jumps
        while (_jumpCommands.Count > 0)
        {
            _jumpCommands[0]();
            _jumpCommands.RemoveAt(0);
        }

        if (_canMove && !_isWallSliding)
        {
            _rigidBody.velocity = new Vector2(
                MovementSpeed * _movementDirection,
                _rigidBody.velocity.y
            );
        }
        if (_rigidBody.velocity.y < -MaxFallingSpeed)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, -MaxFallingSpeed);
        }

        if (_isWallSliding)
        {
            if (_movementDirection * _facingDirection < 0f && _wallClingTimer > 0f) // wall cling
            {
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0f);
            }
            else // wall slide
            {
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, -WallSlideSpeed);
            }
        }

        if (_movementDirection * _facingDirection < 0f && !_isWallSliding)
        {
            Flip();
        }

        if (_isDashing)
        {
            // apply dashing calculations
            _turnTimer = TurnTimerSet;
            _rigidBody.velocity = new Vector2(_facingDirection * DashSpeed, 0f);
            if (
                Mathf.Abs(transform.position.x - _lastFrameDashPosition.x)
                > DistanceBetweenAfterImages
            )
            {
                PlayerAfterImagePool.Instance.GetFromPool();
                _lastFrameDashPosition = transform.position;
            }
        }
    }

    private void Flip(bool isForced = false)
    {
        if (_canFlip || isForced)
        {
            _facingDirection *= -1;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    public void KnockBack(float hitDirection)
    {
        _isKnockBack = true;
        _rigidBody.velocity = new Vector2(KnockBackForce.x * hitDirection, KnockBackForce.y);
        StartCoroutine(HandleKnockBackCooldown());
    }

    private IEnumerator HandleKnockBackCooldown()
    {
        yield return new WaitForSeconds(KnockBackDuration);
        _isKnockBack = false;
        _rigidBody.velocity = new Vector2(0f, _rigidBody.velocity.y);
    }
}
