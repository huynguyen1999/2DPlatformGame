using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyInterfaces
{
    public interface IDamageable
    {
        public void OnAttack(Transform transform, float damage);
        public void OnTouchDamage(Transform transform, float damage);
    }
}
