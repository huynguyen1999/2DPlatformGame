using UnityEngine;
using System;

[Serializable]
public class AttackSprites
{
    [field: SerializeField] public Sprite[] Sprites { get; private set; }
}