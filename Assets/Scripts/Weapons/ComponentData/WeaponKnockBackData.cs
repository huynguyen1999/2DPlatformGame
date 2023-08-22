using UnityEngine;
using System;

public class WeaponKnockBackData : ComponentData<AttackKnockBack>
{
    public WeaponKnockBackData()
    {
        ComponentDependency = typeof(WeaponKnockBack);
    }
}