using UnityEngine;
using System;
using System.Linq;
public class WeaponDraw : WeaponComponent<WeaponDrawData, AttackDraw>
{
    public event Action<float> OnEvaluateCurve;
    private WeaponProjectileSpawner projectileSpawner;
    private Projectile projectile;
    private bool hasEvaluatedDraw;
    private float drawPercentage;

    protected override void HandleEnter()
    {
        base.HandleEnter();
        hasEvaluatedDraw = false;
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
        if (newInput || hasEvaluatedDraw) return;
        hasEvaluatedDraw = true;
        drawPercentage =
            currentAttackData.DrawCurve.Evaluate(
                Mathf.Clamp((Time.time - weapon.AttackStartTime) / currentAttackData.DrawTime, 0f, 1f));
        OnEvaluateCurve?.Invoke(drawPercentage);
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