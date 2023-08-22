using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Stats : CoreComponent
{

    [field: SerializeField]
    public Stat Health { get; private set; }
    [field: SerializeField]
    public Stat Poise { get; private set; }
    [SerializeField] private float poiseRecoveryRate;
    protected override void Awake()
    {
        base.Awake();
        Health.Init();
        Poise.Init();
    }

    public override void Update()
    {
        base.Update();
        if (Poise.CurrentValue.Equals(Poise.MaxValue))
        {
            return;
        }
        Poise.Increase(poiseRecoveryRate * Time.deltaTime);
    }
}