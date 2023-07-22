using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    public float MaxHealth = 1000f;
    public float WallCheckDistance = 0.2f,
        LedgeCheckDistance = 0.4f;
    public float MinAggroDistance = 3f,
        MaxAggroDistance = 4f;

    public float CloseRangeActionDistance = 1f;
    public Vector2 KnockBackForce = new(5f, 5f);
    public LayerMask WhatIsGround,
        WhatIsTarget;
    public GameObject HitParticle;
}
