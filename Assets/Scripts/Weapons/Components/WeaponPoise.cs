using UnityEngine;

public class WeaponPoise : WeaponComponent<WeaponPoiseData, AttackPoise>
{
    private WeaponActionHitBox hitBox;
    private void HandleDetectTargets(Collider2D[] colliders)
    {
        foreach (var item in colliders)
        {
            if (item.TryGetComponent(out IPoiseDamageable poiseDamageable))
            {
                poiseDamageable.OnPoiseHit(currentAttackData.PoiseDamage);
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