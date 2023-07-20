using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(
    fileName = "newChargePlayerStateData",
    menuName = "Data/State Data/Charge Player State"
)]
public class D_ChargePlayerState : ScriptableObject
{
    public float ChargeTime = 2f,
        ChargeSpeed = 7f;
}
