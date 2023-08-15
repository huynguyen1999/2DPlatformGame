using UnityEngine;
using System;

// [Serializable]
public class WeaponSpriteData : ComponentData
{
    [field: SerializeField] public AttackSprites[] AttackData { get; private set; }
}