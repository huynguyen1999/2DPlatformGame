using UnityEngine;
using System;

[Serializable]
public class AttackSprites : AttackData
{
    [field: SerializeField] public Sprite[] Sprites { get; private set; }
}