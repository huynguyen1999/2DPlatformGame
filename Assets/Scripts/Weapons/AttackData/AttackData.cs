using System;
using UnityEngine;

/// <summary>
/// The data for each attack
/// </summary>
public class AttackData
{
    [SerializeField, HideInInspector]
    private string name = "";
    public void SetAttackName(int i) => name = $"Attack{i}";
}