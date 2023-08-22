using UnityEngine;

[System.Serializable]
public class Movement : CoreComponent
{
    public Rigidbody2D RB { get; private set; }
    public int FacingDirection { get; private set; }
    private Vector2 workspace;

    public bool CanSetVelocity { get; set; } = true;
    public bool CanFlip { get; set; } = true;

    public Vector2 CurrentVelocity
    {
        get
        {
            if (RB != null)
                return RB.velocity;
            return Vector2.zero;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        RB = GetComponentInParent<Rigidbody2D>();
        FacingDirection = 1;
    }
    public void SetVelocityX(float velocity)
    {
        if (!CanSetVelocity) return;
        workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workspace;
    }

    public void SetVelocityY(float velocity)
    {
        if (!CanSetVelocity) return;
        workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspace;
    }

    public void SetVelocity(Vector2 velocity2D)
    {
        if (!CanSetVelocity) return;
        RB.velocity = velocity2D;
    }

    public void SetVelocity(float velocity, Vector2 direction)
    {
        if (!CanSetVelocity) return;
        workspace = direction * velocity;
        RB.velocity = workspace;
    }
    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        if (!CanSetVelocity) return;
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        RB.velocity = workspace;
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }
    public void Flip()
    {
        if (!CanFlip)
            return;
        FacingDirection *= -1;
        transform.root.Rotate(0f, 180f, 0f);
    }
}