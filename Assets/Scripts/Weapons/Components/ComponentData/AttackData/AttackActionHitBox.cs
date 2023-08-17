using UnityEngine;
using System;

[Serializable]
public class AttackActionHitBox : AttackData
{
    public bool Debug;
    [field: SerializeField] public Rect HitBox { get; private set; }
}