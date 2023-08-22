using UnityEngine;
using System;

public class WeaponInputHoldData : ComponentData
{
    public WeaponInputHoldData()
    {
        ComponentDependency = typeof(WeaponInputHold);
    }
}