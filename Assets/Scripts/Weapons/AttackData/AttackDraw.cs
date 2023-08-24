using UnityEngine;
using System;

[Serializable]
public class AttackDraw : AttackData
{
    [field: SerializeField]
    public AnimationCurve DrawCurve { get; private set; }
    [field: SerializeField]
    public float DrawTime { get; private set; }
    [field: SerializeField]
    public Vector2 DrawProjectileOffset { get; private set; }
}