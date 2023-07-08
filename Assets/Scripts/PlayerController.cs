using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Animation
{
    IsGrounded,
    IsWalking,
    IsWallSliding,
    VerticalVelocity
}

public class PlayerController : MonoBehaviour
{
    public Transform WallCheck;
    public Transform GroundCheck;
    public LayerMask Ground;
    public int MaxJumps = 2;
    public float GroundCheckRadius = .3f;
    public float MovementSpeed = 10f;
    public float JumpForce = 15f;
    public float WallCheckDistance = 0.4f;
    public float WallSlideSpeed = 2f;
    public float MovementForceInAir = 30f;
    public float AirDragMultiplier = 0.8f;
    public float JumpHeightMultiplier = 0.5f;
    public float WallHopForce;
    public float WallJumpForce;
    public Vector2 WallHopDirection;
    public Vector2 WallJumpDirection;

    private Rigidbody2D _rb;
    private Animator _animator;

    private float _movementDirection = 0f;
    private bool _isWalking = false;
    private bool _isGrounded = false;
    private bool _isJumpable = false;
    private bool _isAgainstWall = false;
    private bool _isWallSliding = false;
    private int _facingDirection = 1;
    private int _jumpsLeft;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _jumpsLeft = MaxJumps;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        CheckJumpable();
        CheckIfWallSliding();
        UpdateAnimations();
    }
    private void CheckInput()
    {
        _movementDirection = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        if (Input.GetButtonUp("Jump"))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * JumpHeightMultiplier);
        }
    }
    private void Jump()
    {
        if (!_isJumpable) return;
        if (!_isWallSliding)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, JumpForce);
            _jumpsLeft -= 1;
        }
        else if (Mathf.Approximately(_movementDirection, 0f))
        {
            _isWallSliding = false;
            Vector2 forceToAdd = new(WallHopForce * WallHopDirection.x * -_facingDirection, WallHopForce * WallHopDirection.y);
            _rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            _jumpsLeft -= 1;
        }
        else if ((_isWallSliding || _isAgainstWall))
        {
            _isWallSliding = false;
            Vector2 forceToAdd = new(WallJumpForce * WallJumpDirection.x * Mathf.Sign(_movementDirection), WallJumpForce * WallJumpDirection.y);
            _rb.AddForce(forceToAdd, ForceMode2D.Impulse);
            _jumpsLeft -= 1;
        }
    }
    private void CheckMovementDirection()
    {
        if (_facingDirection == 1 && _movementDirection < 0)
        {
            Flip();
        }
        else if (_facingDirection == -1 && _movementDirection > 0)
        {
            Flip();
        }

        _isWalking = _isGrounded && !Mathf.Approximately(_movementDirection, 0f);
    }
    private void Flip()
    {
        if (_isWallSliding) return;
        _facingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }
    private void CheckJumpable()
    {
        if (_isWallSliding || (_isGrounded && _rb.velocity.y <= 0))
        {
            _jumpsLeft = MaxJumps;
        }

        if (_jumpsLeft <= 0)
        {
            _isJumpable = false;
        }
        else { _isJumpable = true; }
    }
    private void CheckIfWallSliding()
    {
        _isWallSliding = _isAgainstWall && !_isGrounded && _rb.velocity.y < 0;
    }
    private void UpdateAnimations()
    {
        _animator.SetBool(Animation.IsWalking.ToString(), _isWalking);
        _animator.SetBool(Animation.IsGrounded.ToString(), _isGrounded);
        _animator.SetBool(Animation.IsWallSliding.ToString(), _isWallSliding);
        _animator.SetFloat(Animation.VerticalVelocity.ToString(), _rb.velocity.y);
    }
    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }
    private void ApplyMovement()
    {
        Debug.Log(_movementDirection);
        if (_isGrounded)
        {
            _rb.velocity = new Vector2(_movementDirection * MovementSpeed, _rb.velocity.y);
        }
        else if (!_isWallSliding && !Mathf.Approximately(_movementDirection, 0f))
        {
            Vector2 forceToAdd = new(_movementDirection * MovementForceInAir, 0);
            _rb.AddForce(forceToAdd);
        }
        else if (!_isWallSliding && Mathf.Approximately(_movementDirection, 0f))
        {
            _rb.velocity = new Vector2(_rb.velocity.x * 0.95f, _rb.velocity.y);
        }
        if (Mathf.Abs(_rb.velocity.x) > MovementSpeed)
        {
            _rb.velocity = new Vector2(MovementSpeed * _movementDirection, _rb.velocity.y);
        }
        ApplySliding();
    }
    private void ApplySliding()
    {
        if (!_isWallSliding) return;
        if (_rb.velocity.y < -WallSlideSpeed)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, -WallSlideSpeed);
        }
    }
    private void CheckSurroundings()
    {
        _isGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, Ground);
        _isAgainstWall = Physics2D.Raycast(WallCheck.position, transform.right, WallCheckDistance, Ground);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundCheck.position, GroundCheckRadius);
        Gizmos.DrawLine(WallCheck.position, new Vector3(WallCheck.position.x + WallCheckDistance, WallCheck.position.y, WallCheck.position.z));
    }
}
