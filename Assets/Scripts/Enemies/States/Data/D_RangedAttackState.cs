using UnityEngine;

[CreateAssetMenu(
    fileName = "newRangeAttackStateData",
    menuName = "Data/State Data/Ranged Attack State"
)]
public class D_RangedAttackState : ScriptableObject
{
    public float ProjectileDamage = 20f;
    public float ProjectileTravelDistance = 10f;
    public float ProjectileSpeed = 10f;
    public GameObject Projectile;
}
