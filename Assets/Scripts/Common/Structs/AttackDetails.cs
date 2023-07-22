using System;
using System.Collections.Generic;
using UnityEngine;

public struct AttackDetails
{
    public Transform AttackSourceTransform;
    public float Damage;

    public AttackDetails(Transform attackSourceTransform, float damage)
    {
        AttackSourceTransform = attackSourceTransform;
        Damage = damage;
    }
}
