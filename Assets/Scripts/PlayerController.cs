using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Animation
{
    IsGrounded,
    IsWalking,
    IsWallSliding,
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
    public float WallSlideTimerSet;
    public float WallClingTimerSet;
    public float WallSlideTransitionTimerSet;
    public float TurnTimerSet;

    public Vector2 WallJumpDirection;
    public LayerMask WhatIsGround;
    public Transform LedgeCheck;

    private int _amountOfJumpsLeft;
    private int _facingDirection = 1;
    private bool _isWalking = false;
    private bool _isGrounded = false;
    private bool _isAgainstWall = false;
    private bool _isWallSliding = false;
    private bool _isTouchingLedge = false;
    private bool _canNormalJump = true;
    private bool _canWallJump = true;
    private bool _canMove = true;
    private bool _canFlip = true;
    private bool _canWallSlide = true;
    private bool _canClimbLedge = false;
    private float _movementDirection = 0f;
    private float _wallSlideTimer;
    private float _wallClingTimer;
    private float _wallSlideTransitionTimer;
    private float _turnTimer;
    private Animator _animator;
    private Rigidbody2D _rigidBody;
    private List<Command> _jumpCommands = new();
    private BoxCollider2D _collider;

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
                    WallJumpPower * WallJumpDirection.x * _facingDirection * -1,
                    WallJumpPower * WallJumpDirection.y
                );
            _rigidBody.AddForce(jumpForceToAdd, ForceMode2D.Impulse);
            _isWallSliding = false;
            Flip();
            _canMove = false;
            _canFlip = false;
            _turnTimer = TurnTimerSet;
        }
    }

    private void UpdateAnimations()
    {
        _animator.SetBool(Animation.IsWalking.ToString(), _isWalking);
        _animator.SetBool(Animation.IsGrounded.ToString(), _isGrounded);
        _animator.SetBool(Animation.IsWallSliding.ToString(), _isWallSliding);
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
                    _collider.bounds.center,
                    transform.right,
                    _collider.bounds.extents.x + BoxCastDistance,
                    WhatIsGround
                )
                .collider != null;

        _isTouchingLedge =
            _isAgainstWall
            && Physics2D
                .Raycast(
                    LedgeCheck.position,
                    transform.right,
                    _collider.bounds.extents.x + BoxCastDistance,
                    WhatIsGround
                )
                .collider == null;

        _isWalking = _isGrounded && !Mathf.Approximately(_movementDirection, 0f);

        // before
        if (_isWallSliding == true)
        {
            _isWallSliding = !_isGrounded && _isAgainstWall && _wallSlideTimer > 0f;
            if (_wallSlideTimer <= 0f) // unable to slide
            {
                _canWallSlide = false;
                _wallSlideTransitionTimer = WallSlideTransitionTimerSet;
            }
        }
        else if (
            _canWallSlide
            && (_movementDirection * _facingDirection > 0f)
            && !_isGrounded
            && _isAgainstWall
        )
        {
            _isWallSliding = true;
            _wallClingTimer = WallClingTimerSet;
            _wallSlideTimer = WallSlideTimerSet;
        }
        // after
        _rigidBody.gravityScale = _isWallSliding ? 0f : 8f;
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

        if (_isWallSliding)
        {
            if (_movementDirection * _facingDirection > 0f && _wallClingTimer > 0f) // wall cling
            {
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0f);
            }
            else // wall slide
            {
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, -WallSlideSpeed);
            }
        }

        if (_movementDirection * _facingDirection < 0f)
        {
            Flip();
        }
    }

    private void Flip()
    {
        if (_canFlip && !_isWallSliding)
        {
            _facingDirection *= -1;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }
}
