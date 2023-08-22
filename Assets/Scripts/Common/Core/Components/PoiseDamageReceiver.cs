using UnityEngine;
using System;

[Serializable]
public class PoiseDamageReceiver : CoreComponent, IPoiseDamageable
{
    public bool IsHit { get; set; } = false;
    public void OnPoiseHit(float damage)
    {
        core.Stats.Poise.Decrease(damage);
    }
}