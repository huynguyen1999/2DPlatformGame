using UnityEngine;
using System;

public class WeaponActionHitBox : WeaponComponent<WeaponActionHitBoxData, AttackActionHitBox>
{
    public event Action<Collider2D[]> OnTargetsDetected;
    private Vector2 offset;
    private Collider2D[] detected;
    private void HandleAttackAction()
    {
        offset.Set(
            transform.position.x + (currentAttackData.HitBox.center.x * weapon.Core.Movement.FacingDirection),
            transform.position.y + currentAttackData.HitBox.center.y
        );
        detected = Physics2D.OverlapBoxAll(offset, currentAttackData.HitBox.size, 0f, data.DetectableLayers);
        if (detected.Length == 0) return;
        OnTargetsDetected?.Invoke(detected);
    }

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        weapon.EventHandler.OnAttackAction += HandleAttackAction;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        weapon.EventHandler.OnAttackAction -= HandleAttackAction;
    }

    private void OnDrawGizmosSelected()
    {
        if (data == null) return;
        foreach (var item in data.AttackData)
        {
            if (!item.Debug) continue;
            Gizmos.DrawWireCube(transform.position + (Vector3)item.HitBox.center, item.HitBox.size);
        }
    }
}