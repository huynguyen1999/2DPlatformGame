using UnityEngine;

[System.Serializable]
public class Combat : CoreComponent, IDamageable, IKnockbackable
{
    [SerializeField] private GameObject damageParticles;
    public bool IsHit { get; set; } = false;
    public bool IsKnockedBack { get; set; } = false;
    public void OnHit(AttackDetails attackDetails)
    {
        core.Stats.DecreaseHealth(attackDetails.Damage);
        core.ParticleManager.StartParticlesWithRandomRotation(damageParticles);
    }
    public void Knockback(Vector2 angle, float force, int direction)
    {
        core.Movement.SetVelocity(force, angle, direction);
    }
}