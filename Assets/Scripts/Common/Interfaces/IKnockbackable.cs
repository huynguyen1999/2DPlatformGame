using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IKnockbackable
{
    public bool IsKnockedBack { get; set; }
    void KnockBack(Vector2 angle, float force, int direction);
}