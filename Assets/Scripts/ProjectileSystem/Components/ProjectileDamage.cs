using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ProjectileDamage : ProjectileComponent
{
    [field: SerializeField] public LayerMask LayerMask { get; private set; }
    private bool hasHit = false;
    private ProjectileHitBox hitBox;

    private float amount;

    private void HandleRaycastHit2D(RaycastHit2D[] hits)
    {
        if (hasHit) return;
        foreach (var hit in hits)
        {
            if (hit.collider.TryGetComponent(out IDamageable damageable))
            {
                AttackDetails attackDetails = new(transform.root.transform, (int)transform.right.x, amount);
                damageable.OnHit(attackDetails);
                hasHit = true;
                return;
            }
        }
    }

    // Handle checking to see if the data is relevant or not, and if so, extracts the information we care about
    protected override void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
    {
        base.HandleReceiveDataPackage(dataPackage);

        if (dataPackage is not DamageDataPackage package)
            return;

        amount = package.Amount;
    }

    #region Plumbing

    protected override void Awake()
    {
        base.Awake();
        hasHit = false;
        hitBox = GetComponent<ProjectileHitBox>();
        hitBox.OnRaycastHit2D += HandleRaycastHit2D;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        hitBox.OnRaycastHit2D -= HandleRaycastHit2D;
    }

    #endregion
}