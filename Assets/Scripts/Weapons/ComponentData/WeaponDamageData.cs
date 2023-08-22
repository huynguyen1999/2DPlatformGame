using UnityEngine;
using System;

public class WeaponDamageData : ComponentData<AttackDamage>
{
    public WeaponDamageData()
    {
        ComponentDependency = typeof(WeaponDamage);
    }
}