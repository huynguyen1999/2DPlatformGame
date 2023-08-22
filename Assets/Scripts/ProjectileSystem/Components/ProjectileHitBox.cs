using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ProjectileHitBox : ProjectileComponent
{
    public event Action<RaycastHit2D[]> OnRaycastHit2D;

    [field: SerializeField] public Rect HitBoxRect { get; private set; }
    [field: SerializeField] public LayerMask LayerMask { get; private set; }

    private RaycastHit2D[] hits;
    private float checkDistance;

    private void CheckHitBox()
    {
        hits = Physics2D.BoxCastAll(transform.TransformPoint(HitBoxRect.center), HitBoxRect.size,
            transform.rotation.eulerAngles.z, transform.right, checkDistance, LayerMask);

        if (hits.Length <= 0) return;
        OnRaycastHit2D?.Invoke(hits);
    }

    #region Plumbing

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        // Used to compensate for projectile velocity to help stop clipping
        checkDistance = rb.velocity.magnitude * Time.deltaTime;

        CheckHitBox();
    }

    private void OnDrawGizmosSelected()
    {
        // The following is some code that ChatGPT Generated for me to visualize the HitBoxRect based on the rotation.
        // Set up gizmo color
        Gizmos.color = Color.red;

        // Create a new matrix that applies the projectile's rotation
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position,
            Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z), Vector3.one);
        Gizmos.matrix = rotationMatrix;

        // Draw the wireframe cube
        Gizmos.DrawWireCube(HitBoxRect.center, HitBoxRect.size);
    }

    #endregion
}