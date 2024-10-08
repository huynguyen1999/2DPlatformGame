using UnityEngine;
using System;

public class WeaponProjectileSpawner : WeaponComponent<WeaponProjectileSpawnerData, AttackProjectileSpawner>
{
    public event Action<Projectile> OnProjectileSpawned;
    private Vector2 spawnPos;
    private Vector2 spawnDir;
    private Projectile currentProjectile;

    //spawn and fire projectile
    private void HandleSpawnProjectile()
    {
        SetSpawnPosition(transform.root.position, currentAttackData.Offset, weapon.Core.Movement.FacingDirection);
        SetSpawnDirection(currentAttackData.Direction, weapon.Core.Movement.FacingDirection);
        InitializeProjectile();
    }

    private void InitializeProjectile()
    {
        var angle = Mathf.Atan2(spawnDir.y, spawnDir.x) * Mathf.Rad2Deg;
        currentProjectile = Instantiate(currentAttackData.ProjectilePrefab, spawnPos, Quaternion.AngleAxis(angle, Vector3.forward));
        OnProjectileSpawned?.Invoke(currentProjectile);
    }
    private void SetSpawnPosition(Vector3 referencePosition, Vector2 offset, int facingDirection)
    {
        spawnPos = referencePosition;
        spawnPos = new Vector2(
            spawnPos.x + offset.x * facingDirection,
            spawnPos.y + offset.y
        );
    }
    private void SetSpawnDirection(Vector2 direction, int facingDirection)
    {
        spawnDir = new Vector2(direction.x * facingDirection,
                direction.y);
    }

    protected override void Start()
    {
        base.Start();
        weapon.EventHandler.OnSpawnProjectile += HandleSpawnProjectile;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        weapon.EventHandler.OnSpawnProjectile -= HandleSpawnProjectile;
    }
}