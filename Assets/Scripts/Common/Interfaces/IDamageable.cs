using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public bool IsHit { get; set; }
    public void OnHit(AttackDetails attackDetails);
}
