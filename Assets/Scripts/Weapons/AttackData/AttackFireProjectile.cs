using UnityEngine;
using System;

[Serializable]
public class AttackFireProjectile : AttackData
{
    [field: SerializeField] public DamageDataPackage DamageData { get; private set; }
}