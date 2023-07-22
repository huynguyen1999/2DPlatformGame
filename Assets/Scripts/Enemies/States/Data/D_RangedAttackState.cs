using UnityEngine;

[CreateAssetMenu(
    fileName = "newRangeAttackStateData",
    menuName = "Data/State Data/Ranged Attack State"
)]
public class D_RangedAttackState : ScriptableObject
{
    public float AttackDamage;
    public float AttackRange;
}
