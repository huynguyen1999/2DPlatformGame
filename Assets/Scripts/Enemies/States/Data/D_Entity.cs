using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    public float WallCheckDistance = 0.2f,
        LedgeCheckDistance = 0.4f;
    public float MinAggroDistance = 3f,
        MaxAggroDistance = 4f;
    public LayerMask WhatIsGround,
        WhatIsPlayer;
}
