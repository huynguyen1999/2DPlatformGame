using UnityEngine;
using System;

public class WeaponDrawData : ComponentData<AttackDraw>
{
    public WeaponDrawData()
    {
        ComponentDependency = typeof(WeaponDraw);
    }
}