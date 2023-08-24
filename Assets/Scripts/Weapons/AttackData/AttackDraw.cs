using UnityEngine;
using System;

[Serializable]
public class AttackDraw : AttackData
{
    [field: SerializeField]
    public Vector2 DrawProjectileOffset { get; private set; }
}