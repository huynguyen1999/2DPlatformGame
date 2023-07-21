using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
