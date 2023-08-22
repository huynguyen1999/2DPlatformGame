using UnityEngine;

public class KnockBackReceiver : CoreComponent, IKnockbackable
{
    [SerializeField] private float maxKnockBackTime = 0.2f;
    private bool isKnockBackActive;
    private float knockBackStartTime;
    private bool isGrounded;
    public bool IsKnockedBack { get; set; } = false;

    public void KnockBack(Vector2 angle, float force, int direction)
    {
        if (isKnockBackActive) return;
        core.Movement.SetVelocity(force, angle, direction);
        core.Movement.CanSetVelocity = false;
        core.Movement.CanFlip = false;
        isKnockBackActive = true;
        knockBackStartTime = Time.time;
    }

    public override void Update()
    {
        CheckKnockBack();
    }
    public override void FixedUpdate()
    {
        isGrounded = core.CollisionDetection.CheckIfGrounded();
    }

    private void CheckKnockBack()
    {
        if (!isKnockBackActive) return;

        if (Time.time > knockBackStartTime + maxKnockBackTime || isGrounded)
        {
            core.Movement.CanSetVelocity = true;
            core.Movement.CanFlip = true;
            isKnockBackActive = false;
            core.Movement.SetVelocityX(0f);
        }
    }
}