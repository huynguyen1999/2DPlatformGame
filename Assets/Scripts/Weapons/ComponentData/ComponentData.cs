using UnityEngine;
using System;

/// <summary>
/// The data for each component
/// </summary>
[Serializable]
public class ComponentData
{
    [SerializeField, HideInInspector] private string name;
    public Type ComponentDependency { get; protected set; }
    public ComponentData()
    {
        SetComponentName();
    }
    public void SetComponentName() => name = GetType().Name;
    public virtual void SetAttackDataNames() { }
    public virtual void InitializeAttackData(int numberOfAttacks) { }
}

/// <summary>
/// The data for each component
/// One component can have multiple attack data
/// </summary>
/// <typeparam name="T">AttackData</typeparam>
[Serializable]
public class ComponentData<T> : ComponentData where T : AttackData
{
    [SerializeField] private T[] attackData;
    public T[] AttackData { get => attackData; private set => attackData = value; }

    public override void SetAttackDataNames()
    {
        base.SetAttackDataNames();

        for (var i = 0; i < AttackData.Length; i++)
        {
            AttackData[i].SetAttackName(i + 1);
        }
    }

    public override void InitializeAttackData(int numberOfAttacks)
    {
        base.InitializeAttackData(numberOfAttacks);

        var oldLen = attackData != null ? attackData.Length : 0;

        if (oldLen == numberOfAttacks)
            return;

        Array.Resize(ref attackData, numberOfAttacks);

        if (oldLen < numberOfAttacks)
        {
            for (var i = oldLen; i < attackData.Length; i++)
            {
                var newObj = Activator.CreateInstance(typeof(T)) as T;
                attackData[i] = newObj;
            }
        }

        SetAttackDataNames();
    }
}