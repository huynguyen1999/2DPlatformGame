using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public struct AttackDetails
{
    public Transform AttackSourceTransform;
    public float Damage;
    public bool HasStunEffect;
    public float StunDuration;

    public AttackDetails(
        Transform attackSourceTransform,
        float damage,
        bool hasStunEffect = false,
        float stunDuration = 0f
    )
    {
        AttackSourceTransform = attackSourceTransform;
        Damage = damage;
        HasStunEffect = hasStunEffect;
        StunDuration = stunDuration;
    }
}
