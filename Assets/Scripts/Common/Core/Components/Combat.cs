using UnityEngine;

public class Combat : CoreComponent, IDamageable, IKnockbackable
{
    public bool IsHit { get; set; } = false;
    public bool IsKnockedBack { get; set; } = false;
    public void OnHit(AttackDetails attackDetails)
    {
        Debug.Log($"{transform.root.name} is dealt {attackDetails.Damage} damage!");
    }
    public void Knockback(Vector2 angle, float force, int direction)
    {
        core.Movement.SetVelocity(force, angle, direction);
    }
}