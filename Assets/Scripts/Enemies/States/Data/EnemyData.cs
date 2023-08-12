using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "newEnemyData",
    menuName = "Data/Enemy Data/Base Data"
)]
public class EnemyData : ScriptableObject
{
    [Header("Charge State")]
    public float ChargeTime = 1f,
       ChargeSpeed = 7f;

    [Header("Dodge State")]
    public float DodgeCoolDown = 5f;
    public float DodgeDuration = 0.5f;
    public Vector2 DodgeJumpForce = new(10f, 10f);
}
