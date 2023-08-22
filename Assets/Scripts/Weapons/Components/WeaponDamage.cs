using UnityEngine;

public class WeaponDamage : WeaponComponent<WeaponDamageData, AttackDamage>
{
    private WeaponActionHitBox hitBox;
    private void HandleDetectTargets(Collider2D[] colliders)
    {
        foreach (var item in colliders)
        {
            if (item.TryGetComponent(out IDamageable damageable))
            {
                AttackDetails attackDetails = new(transform.root.transform, weapon.Core.Movement.FacingDirection, currentAttackData.Amount);
                damageable.OnHit(attackDetails);
            }
        }
    }

    protected override void Start()
    {
        base.Start();
        hitBox = GetComponent<WeaponActionHitBox>();
        hitBox.OnTargetsDetected += HandleDetectTargets;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        hitBox.OnTargetsDetected -= HandleDetectTargets;
    }
}