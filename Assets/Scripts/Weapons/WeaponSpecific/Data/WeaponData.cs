using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Weapon", order = 1)]
public class WeaponData : ScriptableObject
{
    [Header("Sword")]
    public float[] movementSpeed = new float[3] { 3f, 5f, 7f };
    public float attackCounterResetTime = 0.5f;
    public int maxAttackCounter = 3;
    [Header("Book")]
    public float holdTime = 0.5f;
}
