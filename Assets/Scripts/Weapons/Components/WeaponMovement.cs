using UnityEngine;

public class WeaponMovement : WeaponComponent<WeaponMovementData, AttackMovement>
{
    private void HandleStartMovement()
    {
        weapon.Core.Movement.SetVelocity(currentAttackData.Velocity, currentAttackData.Direction, weapon.Core.Movement.FacingDirection);
    }
    private void HandleStopMovement()
    {
        weapon.Core.Movement.SetVelocity(Vector2.zero);
    }

    protected override void Awake()
    {
        base.Awake();
        data = weapon.Data.GetData<WeaponMovementData>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        weapon.EventHandler.OnStartMovement += HandleStartMovement;
        weapon.EventHandler.OnStopMovement += HandleStopMovement;
    }
    protected override void OnDisable()
    {
        base.OnEnable();
        weapon.EventHandler.OnStartMovement -= HandleStartMovement;
        weapon.EventHandler.OnStopMovement -= HandleStopMovement;
    }
}