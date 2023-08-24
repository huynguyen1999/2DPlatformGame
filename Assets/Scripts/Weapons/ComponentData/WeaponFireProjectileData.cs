using UnityEngine;
using System;

public class WeaponFireProjectileData : ComponentData<AttackFireProjectile>
{
    public WeaponFireProjectileData()
    {
        ComponentDependency = typeof(WeaponFireProjectile);
    }
}