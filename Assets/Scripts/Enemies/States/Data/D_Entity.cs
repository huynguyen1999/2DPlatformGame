using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    public float WallCheckDistance = 0.2f;
    public float LedgeCheckDistance = 0.4f;
    public LayerMask WhatIsGround;
}
