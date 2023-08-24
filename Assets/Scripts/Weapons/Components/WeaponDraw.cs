using UnityEngine;
using System;
using System.Linq;
public class WeaponDraw : WeaponComponent<WeaponDrawData, AttackDraw>
{
    private WeaponProjectileSpawner projectileSpawner;
    private Projectile projectile;
    protected override void HandleEnter()
    {
        base.HandleEnter();
    }
    private void HandleProjectileSpawned(Projectile projectile)
    {
        this.projectile = projectile;
    }
    private void HandleDraw()
    {
        projectile.transform.position = new Vector2(
          transform.root.position.x + currentAttackData.DrawProjectileOffset.x * weapon.Core.Movement.FacingDirection,
          transform.root.position.y + currentAttackData.DrawProjectileOffset.y
        );
    }
    private void HandleCurrentInputChange(bool newInput)
    {
        
    }

    protected override void Awake()
    {
        base.Awake();
        projectileSpawner = GetComponent<WeaponProjectileSpawner>();
        projectileSpawner.OnProjectileSpawned += HandleProjectileSpawned;
        weapon.OnCurrentInputChange += HandleCurrentInputChange;
        weapon.EventHandler.OnDraw += HandleDraw;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        projectileSpawner.OnProjectileSpawned -= HandleProjectileSpawned;
        weapon.OnCurrentInputChange -= HandleCurrentInputChange;
        weapon.EventHandler.OnDraw -= HandleDraw;
    }

}