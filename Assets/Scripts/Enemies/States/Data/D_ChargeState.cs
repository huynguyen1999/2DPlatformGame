using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "newChargeStateData",
    menuName = "Data/State Data/Charge State"
)]
public class D_ChargeState : ScriptableObject
{
    public float ChargeTime = 1f,
        ChargeSpeed = 7f;
}
