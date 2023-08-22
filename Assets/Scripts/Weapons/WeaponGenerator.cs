using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Linq;

/// <summary>
/// Create weapon components attached to the weapon game object in the beginning
/// </summary>
public class WeaponGenerator : MonoBehaviour
{
    private Weapon weapon;
    [SerializeField] private WeaponDataSO data;
    private List<WeaponComponent> componentAlreadyOnWeapon = new List<WeaponComponent>();
    private List<WeaponComponent> componentsAddedToWeapon = new List<WeaponComponent>();
    private List<Type> componentDependencies = new List<Type>();
    private Animator anim;
    private void Start()
    {
        weapon = GetComponent<Weapon>();
        anim = GetComponentInChildren<Animator>();
        GenerateWeapon(data);
    }
    /// <summary>
    /// Get all dependencies added in weapon data script object
    /// If the component is already on the weapon, do nothing
    /// Else, add the weapon component into the game object
    /// Remove all the excessive components
    /// </summary>
    /// <param name="data"></param>
    public void GenerateWeapon(WeaponDataSO data)
    {
        weapon.SetData(data);
        componentAlreadyOnWeapon.Clear();
        componentsAddedToWeapon.Clear();
        componentDependencies.Clear();
        componentAlreadyOnWeapon = GetComponents<WeaponComponent>().ToList();
        componentDependencies = data.GetAllDependencies();
        foreach (var dependency in componentDependencies)
        {
            if (componentsAddedToWeapon.FirstOrDefault(component => component.GetType() == dependency))
            {
                continue;
            }
            var weaponComponent = componentAlreadyOnWeapon.FirstOrDefault(component => component.GetType() == dependency);
            if (weaponComponent == null)
            {
                weaponComponent = gameObject.AddComponent(dependency) as WeaponComponent;
            }
            weaponComponent.Init();
            componentsAddedToWeapon.Add(weaponComponent);
        }
        var componentsToRemove = componentAlreadyOnWeapon.Except(componentsAddedToWeapon);
        foreach (var weaponComponent in componentsToRemove)
        {
            Destroy(weaponComponent);
        }
        anim.runtimeAnimatorController = data.AnimatorController;
    }
}