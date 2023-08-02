using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Weapon", order = 1)]
public class WeaponData : ScriptableObject
{
    public float[] movementSpeed = new float[3] { 3f, 5f, 7f };

    public float attackCounterResetTime = 0.5f;
}
