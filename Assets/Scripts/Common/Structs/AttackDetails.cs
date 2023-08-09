using System;
using System.Collections.Generic;
using UnityEngine;

public struct AttackDetails
{
    public Transform AttackSourceTransform;
    public int AttackDirection;
    public float Damage;

    public AttackDetails(Transform attackSourceTransform, int attackDirection, float damage)
    {
        AttackSourceTransform = attackSourceTransform;
        Damage = damage;
        AttackDirection = attackDirection;
    }
}
