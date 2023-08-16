using UnityEngine;

public class WeaponMovementData : ComponentData
{
    [field: SerializeField]
    public AttackMovement[] AttackData { get; private set; }
}