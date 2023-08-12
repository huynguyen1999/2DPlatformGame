using UnityEngine;

[System.Serializable]
public class Movement : CoreComponent
{
    public Rigidbody2D RB { get; private set; }
    public int FacingDirection { get; private set; }
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

    protected override void Awake()
    {
        base.Awake();
        RB = GetComponentInParent<Rigidbody2D>();
        FacingDirection = 1;
    }
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

    public void SetVelocity(float velocity, Vector2 direction)
    {
        workspace = direction * velocity;
        RB.velocity = workspace;
    }
    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
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
        FacingDirection *= -1;
        transform.root.Rotate(0f, 180f, 0f);
    }
}