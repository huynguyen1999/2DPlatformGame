using UnityEngine;

public class WeaponFireProjectile : WeaponComponent<WeaponFireProjectileData, AttackFireProjectile>
{
    private WeaponProjectileSpawner projectileSpawner;
    private WeaponDraw weaponDraw;
    private Projectile projectile;
    private void HandleProjectileSpawned(Projectile projectile)
    {
        this.projectile = projectile;
    }
    private void HandleAttackAction()
    {
        projectile.SendDataPackage(currentAttackData.DamageData);
        projectile.SendDataPackage(currentAttackData.DrawData);
        projectile.Init();
    }

    private void HandleEvaluateCurve(float value)
    {
        currentAttackData.DrawData.DrawPercentage = value;
    }

    protected override void Start()
    {
        base.Start();
        projectileSpawner = GetComponent<WeaponProjectileSpawner>();
        weaponDraw = GetComponent<WeaponDraw>();
        weaponDraw.OnEvaluateCurve += HandleEvaluateCurve;
        projectileSpawner.OnProjectileSpawned += HandleProjectileSpawned;
        weapon.EventHandler.OnAttackAction += HandleAttackAction;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        projectileSpawner.OnProjectileSpawned -= HandleProjectileSpawned;
        weaponDraw.OnEvaluateCurve -= HandleEvaluateCurve;
        weapon.EventHandler.OnAttackAction -= HandleAttackAction;
    }
}