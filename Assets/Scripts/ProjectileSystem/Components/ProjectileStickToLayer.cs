using UnityEngine;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(ProjectileHitBox))]
public class ProjectileStickToLayer : ProjectileComponent
{
    public UnityEvent OnProjectileStuck;

    [field: SerializeField] public LayerMask LayerMask { get; private set; }
    [field: SerializeField] public float CheckDistance { get; private set; }

    private bool isStuck;
    private ProjectileHitBox hitBox;
    protected override void Awake()
    {
        base.Awake();
        hitBox = GetComponent<ProjectileHitBox>();
        hitBox.OnRaycastHit2D += HandleRaycastHit2D;
    }
    private void HandleRaycastHit2D(RaycastHit2D[] hits)
    {
        if (isStuck) return;
        isStuck = true;
        transform.position = (Vector2)transform.position + rb.velocity * Time.deltaTime;
        rb.velocity = Vector2.zero;
        transform.parent = hits[0].transform;
        rb.isKinematic = true;
        OnProjectileStuck?.Invoke();
    }
}