using UnityEngine;
public class WeaponPoiseData : ComponentData<AttackPoise>
{
    public WeaponPoiseData()
    {
        ComponentDependency = typeof(WeaponPoise);
    }

}