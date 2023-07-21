using UnityEngine;

[CreateAssetMenu(fileName = "newStunStateData", menuName = "Data/State Data/Stun State")]
public class D_StunState : ScriptableObject
{
    public float DefaultStunDuration = 1f;
    public float NormalAttackStunCoolDown = 3f;
}
