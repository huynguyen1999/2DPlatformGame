using UnityEngine;

public class WeaponFireProjectile : WeaponComponent<WeaponFireProjectileData, AttackFireProjectile>
{
    private WeaponProjectileSpawner projectileSpawner;
    private Projectile projectile;
    private void HandleProjectileSpawned(Projectile projectile)
    {
        this.projectile = projectile;
    }
    private void HandleAttackAction()
    {
        projectile.SendDataPackage(currentAttackData.DamageData);
        projectile.Init();
    }

    protected override void Start()
    {
        base.Start();
        projectileSpawner = GetComponent<WeaponProjectileSpawner>();
        projectileSpawner.OnProjectileSpawned += HandleProjectileSpawned;
        weapon.EventHandler.OnAttackAction += HandleAttackAction;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        projectileSpawner.OnProjectileSpawned -= HandleProjectileSpawned;
        weapon.EventHandler.OnAttackAction -= HandleAttackAction;
    }
}