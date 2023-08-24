using UnityEngine;

public class ProjectileMovement : ProjectileComponent
{
    [field: SerializeField] public bool ApplyContinuously { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float MaxTraveledDistance { get; private set; }
    private float drawTraveledDistance;
    private float gravityScale;
    private float traveledDistance;
    private Vector2 startPosition;
    protected override void Awake()
    {
        base.Awake();
        gravityScale = rb.gravityScale;
        projectile.RB.gravityScale = 0f;
        drawTraveledDistance = MaxTraveledDistance;
    }

    // On Init, set projectile velocity once
    protected override void Init()
    {
        base.Init();
        SetVelocity();
        startPosition = projectile.transform.position;
    }

    private void SetVelocity() => rb.velocity = Speed * transform.right;

    protected override void Update()
    {
        if (!isActive) return;
        base.Update();
        traveledDistance = Vector2.Distance(startPosition, projectile.transform.position);
        if (traveledDistance >= drawTraveledDistance)
        {
            rb.gravityScale = gravityScale;
        }
    }
    protected override void FixedUpdate()
    {
        if (!isActive) return;
        base.FixedUpdate();

        if (!ApplyContinuously)
            return;

        SetVelocity();
    }
    protected override void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
    {
        base.HandleReceiveDataPackage(dataPackage);

        if (dataPackage is not DrawDataPackage package)
            return;

        drawTraveledDistance = package.DrawPercentage * MaxTraveledDistance;
    }
}