using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "newMeleeAttackStateData",
    menuName = "Data/State Data/Melee Attack State"
)]
public class D_MeleeAttackState : ScriptableObject
{
    public float AttackDamage = 10f;
    public Vector2 KnockbackAngle = new(1f, 1.25f);
    public float KnockbackForce = 10f;
}
