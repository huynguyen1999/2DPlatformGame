using UnityEngine;

public class WeaponKnockBack : WeaponComponent<WeaponKnockBackData, AttackKnockBack>
{
    private WeaponActionHitBox hitBox;
    private void HandleDetectTargets(Collider2D[] colliders)
    {
        foreach (var item in colliders)
        {
            if (item.TryGetComponent(out IKnockbackable knockbackable))
            {
                knockbackable.KnockBack(currentAttackData.Angle, currentAttackData.Force, weapon.Core.Movement.FacingDirection);
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