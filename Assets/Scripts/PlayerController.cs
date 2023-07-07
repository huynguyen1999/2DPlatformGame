using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Animation
{
    Idle,
    Walk,
}

public class PlayerController : MonoBehaviour
{
    public Transform GroundCheck;
    public LayerMask Ground;
    public float GroundCheckRadius = .3f;
    public float MovementSpeed = 10f;
    public float JumpForce = 15f;
    public int MaxJumps = 2;

    private Rigidbody2D _rb;
    private Animator _animator;

    private float _movementDirection = 0f;
    private bool _isFacingRight = true;
    private bool _isWalking = false;
    private bool _isGrounded = false;
    private bool _isJumping = false;
    private bool _isJumpable = false;
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
        UpdateAnimations();
    }
    private void CheckInput()
    {
        _movementDirection = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }
    private void Jump()
    {
        if (_isJumpable)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, JumpForce);
            _jumpsLeft -= 1;
        }
    }
    private void CheckMovementDirection()
    {
        if (_isFacingRight && _movementDirection < 0)
        {
            Flip();
        }
        else if (!_isFacingRight && _movementDirection > 0)
        {
            Flip();
        }

        if (_rb.velocity.x != 0f)
        {
            _isWalking = true;
        }
        else
        {
            _isWalking = false;
        }
    }
    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    private void CheckJumpable()
    {
        if (_isGrounded && _rb.velocity.y <= 0)
        {
            _jumpsLeft = MaxJumps;
        }

        if (_jumpsLeft <= 0)
        {
            _isJumpable = false;
        }
        else { _isJumpable = true; }
    }
    private void UpdateAnimations()
    {
        if (_isWalking)
        {
            _animator.Play(Animation.Walk.ToString());
        }
        else
        {
            _animator.Play(Animation.Idle.ToString());
        }
    }
    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }
    private void ApplyMovement()
    {
        _rb.velocity = new Vector2(_movementDirection * MovementSpeed, _rb.velocity.y);
    }
    private void CheckSurroundings()
    {
        _isGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, Ground);
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundCheck.position, GroundCheckRadius);
    }
}
