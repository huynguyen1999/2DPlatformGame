using UnityEngine;

public class WeaponMovementData : ComponentData<AttackMovement>
{
    public WeaponMovementData()
    {
        ComponentDependency = typeof(WeaponMovement);
    }
}