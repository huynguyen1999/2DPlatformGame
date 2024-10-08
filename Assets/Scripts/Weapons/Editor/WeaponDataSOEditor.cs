using System;
using System.Collections.Generic;
using System.Linq;

using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

/// <summary>
/// Custom editor for WeaponDataSO
/// Buttons to add component data
/// Buttons to force update component names
/// </summary>
[CustomEditor(typeof(WeaponDataSO))]
public class WeaponDataSOEditor : Editor
{
    private static List<Type> dataCompTypes = new List<Type>();

    private WeaponDataSO dataSO;
    private bool showForceUpdateButtons,
                showAddComponentButtons;

    private void OnEnable()
    {
        dataSO = target as WeaponDataSO;
    }

    /// <summary>
    /// Create buttons
    /// </summary>
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Set Number of Attacks"))
        {
            foreach (var item in dataSO.ComponentData)
            {
                item.InitializeAttackData(dataSO.NumberOfAttacks);
            }
        }
        
        showForceUpdateButtons = EditorGUILayout.Foldout(showForceUpdateButtons, "Force Update");
        if (showForceUpdateButtons)
        {
            if (GUILayout.Button("Force Update Component Names"))
            {
                foreach (var item in dataSO.ComponentData)
                {
                    item.SetComponentName();
                }
            }
            if (GUILayout.Button("Force Update Attack Names"))
            {
                foreach (ComponentData item in dataSO.ComponentData)
                {
                    item.SetAttackDataNames();
                }
            }
        }

        showAddComponentButtons = EditorGUILayout.Foldout(showAddComponentButtons, "Add Components");
        if (showAddComponentButtons)
        {
            foreach (var dataCompType in dataCompTypes)
            {
                if (GUILayout.Button(dataCompType.Name))
                {
                    var comp = Activator.CreateInstance(dataCompType) as ComponentData;

                    if (comp == null)
                        continue;
                    dataSO.AddData(comp);
                }
            }
        }
    }
    
    /// <summary>
    /// Every time the project is recompiled, get all the ComponentData's children 
    /// </summary>
    [DidReloadScripts]
    private static void OnRecompile()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var types = assemblies.SelectMany(assembly => assembly.GetTypes());
        var filteredTypes = types.Where(
            type => type.IsSubclassOf(typeof(ComponentData)) && !type.ContainsGenericParameters && type.IsClass
        );
        dataCompTypes = filteredTypes.ToList();
    }
}