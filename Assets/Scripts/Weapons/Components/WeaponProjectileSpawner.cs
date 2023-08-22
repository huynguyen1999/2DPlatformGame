using UnityEngine;
using System;

public class WeaponProjectileSpawner : WeaponComponent<WeaponProjectileSpawnerData, AttackProjectileSpawner>
{
    public event Action<Projectile> OnSpawnProjectile;
    private Vector2 spawnPos;
    private Vector2 spawnDir;
    private Projectile currentProjectile;

    //spawn and fire projectile
    private void HandleAttackAction()
    {
        SetSpawnPosition(transform.root.position, currentAttackData.Offset, weapon.Core.Movement.FacingDirection);
        SetSpawnDirection(currentAttackData.Direction, weapon.Core.Movement.FacingDirection);
        InitializeProjectile();
    }

    private void InitializeProjectile()
    {
        var angle = Mathf.Atan2(spawnDir.y, spawnDir.x) * Mathf.Rad2Deg;
        Debug.Log($"Direction: {weapon.Core.Movement.FacingDirection}; Angle: {angle}; Spawn rotation: {Quaternion.AngleAxis(angle, Vector3.forward)}");
        currentProjectile = Instantiate(currentAttackData.ProjectilePrefab, spawnPos, Quaternion.AngleAxis(angle, Vector3.forward));
        currentProjectile.SendDataPackage(currentAttackData.DamageData);
        OnSpawnProjectile?.Invoke(currentProjectile);
        currentProjectile.Init();
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
        weapon.EventHandler.OnAttackAction += HandleAttackAction;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        weapon.EventHandler.OnAttackAction -= HandleAttackAction;
    }
}