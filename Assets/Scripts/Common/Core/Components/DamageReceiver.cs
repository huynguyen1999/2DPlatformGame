using UnityEngine;

[System.Serializable]
public class DamageReceiver : CoreComponent, IDamageable
{
    [SerializeField] private GameObject damageParticles;
    public bool IsHit { get; set; } = false;
    public void OnHit(AttackDetails attackDetails)
    {
        Debug.Log($"{transform.root.name} is dealt {attackDetails.Damage} damage");
        core.Stats.Health.Decrease(attackDetails.Damage);
        core.ParticleManager.StartParticlesWithRandomRotation(damageParticles);
    }
}