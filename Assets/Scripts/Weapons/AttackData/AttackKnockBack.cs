using UnityEngine;
using System;

[Serializable]
public class AttackKnockBack : AttackData
{
    [field: SerializeField] public float Force { get; private set; }
    [field: SerializeField] public Vector2 Angle { get; private set; }
}