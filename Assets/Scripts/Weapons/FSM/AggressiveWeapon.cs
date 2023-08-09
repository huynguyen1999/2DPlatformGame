using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class AggressiveWeapon : Weapon
{
    private List<IDamageable> detectedDamageables = new List<IDamageable>();
    private List<IKnockbackable> detectedKnockbackables = new List<IKnockbackable>();
    // private List<int> detectedColliderIDs = new List<int>();

    public void AddToDetected(Collider2D collision)
    {
        // int instanceID = collision.GetInstanceID();
        // if (detectedColliderIDs.Contains(instanceID))
        // {
        //     return;
        // }
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.IsHit = false;
            detectedDamageables.Add(damageable);
        }
        IKnockbackable knockbackable = collision.GetComponent<IKnockbackable>();
        if (knockbackable != null)
        {
            knockbackable.IsKnockedBack = false;
            detectedKnockbackables.Add(knockbackable);
        }
        // detectedColliderIDs.Add(instanceID);
    }
    public void RemoveFromDetected(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            detectedDamageables.Remove(damageable);
        }
        IKnockbackable knockbackable = collision.GetComponent<IKnockbackable>();
        if (knockbackable != null)
        {
            detectedKnockbackables.Remove(knockbackable);
        }
    }

    public void CheckIfDealDamage()
    {
        foreach (IDamageable damageable in detectedDamageables)
        {
            if (damageable.IsHit)
            {
                continue;
            }
            AttackDetails attackDetails = new AttackDetails(transform, player.Core.Movement.FacingDirection, weaponData.damage);
            damageable.OnHit(attackDetails);
            damageable.IsHit = true;
        }
        foreach (IKnockbackable knockbackable in detectedKnockbackables)
        {
            if (knockbackable.IsKnockedBack)
            {
                continue;
            }
            knockbackable.Knockback(weaponData.knockbackAngle, weaponData.knockbackForce, player.Core.Movement.FacingDirection);
            knockbackable.IsKnockedBack = true;
        }
    }
}