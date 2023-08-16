using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Basic Weapon Data", order = 0)]
public class WeaponDataSO : ScriptableObject
{
    [field: SerializeField] public int NumberOfAttacks { get; private set; }

    [field: SerializeReference] public List<ComponentData> ComponentData { get; private set; }

    public T GetData<T>()
    {
        return ComponentData.OfType<T>().FirstOrDefault();
    }

    [ContextMenu("Add Sprite Data")]
    private void AddSpriteData() => ComponentData.Add(new WeaponSpriteData());
    [ContextMenu("Add Movement Data")]
    private void AddMovementDataData() => ComponentData.Add(new WeaponMovementData());
}