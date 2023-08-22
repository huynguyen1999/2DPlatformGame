using UnityEngine;
using System;

[Serializable]
public class DamageDataPackage : ProjectileDataPackage
{
    [field: SerializeField] public float Amount { get; private set; }
}