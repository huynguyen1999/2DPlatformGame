using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoiseDamageable
{
    public bool IsHit { get; set; }
    public void OnPoiseHit(float damage);
}
