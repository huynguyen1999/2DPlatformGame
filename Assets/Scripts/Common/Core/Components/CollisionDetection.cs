using UnityEngine;

[System.Serializable]
public class CollisionDetection : CoreComponent
{
    public Transform GroundCheck { get => GenericNotImplementedError<Transform>.TryGet(groundCheck, transform.root.name); }
    public Transform WallCheck { get => GenericNotImplementedError<Transform>.TryGet(wallCheck, transform.root.name); }
    public Transform LedgeCheckHorizontal { get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckHorizontal, transform.root.name); }
    public Transform LedgeCheckVertical { get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckVertical, transform.root.name); }
    public Transform CeilingCheck { get => GenericNotImplementedError<Transform>.TryGet(ceilingCheck, transform.root.name); }
    public LayerMask WhatIsGround { get => GenericNotImplementedError<LayerMask>.TryGet(whatIsGround, transform.root.name); }
    public float WallCheckDistance { get => GenericNotImplementedError<float>.TryGet(wallCheckDistance, transform.root.name); }
    public float GroundCheckRadius { get => GenericNotImplementedError<float>.TryGet(groundCheckRadius, transform.root.name); }

    #region SerializeFields
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheckHorizontal;
    [SerializeField]
    private Transform ledgeCheckVertical;
    [SerializeField]
    private Transform ceilingCheck;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private float groundCheckRadius = 0.3f;
    [SerializeField]
    private float wallCheckDistance = 0.4f;
    #endregion

    public bool CheckIfGrounded()
    {
        if (GroundCheck == null) return false;
        return Physics2D.OverlapCircle(
            GroundCheck.position,
            groundCheckRadius,
            WhatIsGround
        );
    }

    public bool CheckIfTouchingWall()
    {
        if (WallCheck == null) return false;
        return Physics2D.Raycast(
            WallCheck.position,
            transform.root.right,
            wallCheckDistance,
            WhatIsGround
        );
    }

    public bool CheckIfTouchingHorizontalLedge()
    {
        if (LedgeCheckHorizontal == null) return false;
        return Physics2D.Raycast(
            LedgeCheckHorizontal.position,
            transform.root.right,
            wallCheckDistance,
            WhatIsGround
        );
    }

    public bool CheckIfTouchingVerticalLedge()
    {
        if (LedgeCheckVertical == null) return false;
        return Physics2D.Raycast(
            LedgeCheckVertical.position,
            -transform.root.up,
            wallCheckDistance,
            WhatIsGround
        );
    }

    public bool CheckIfTouchingCeiling()
    {
        if (CeilingCheck == null) return false;
        return Physics2D.OverlapCircle(
            CeilingCheck.position,
            groundCheckRadius,
            WhatIsGround
        );
    }

    protected override void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(
            WallCheck.position,
            WallCheck.position
                + new Vector3(wallCheckDistance * transform.right.x, 0f, 0f)
        );
        if (CeilingCheck != null)
        {
            Gizmos.DrawWireSphere(CeilingCheck.position, groundCheckRadius);
        }
        if (LedgeCheckHorizontal != null)
        {
            Gizmos.DrawLine(
                LedgeCheckHorizontal.position,
                LedgeCheckHorizontal.position
                    + new Vector3(wallCheckDistance * transform.right.x, 0f, 0f)
            );
        }
        if (LedgeCheckVertical != null)
        {
            Gizmos.DrawLine(
                LedgeCheckVertical.position,
                LedgeCheckVertical.position
                    + new Vector3(0f, wallCheckDistance * Vector2.down.y, 0f)
            );
        }
    }
}