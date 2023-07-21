using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(
    fileName = "newPlayerDetectedStateData",
    menuName = "Data/State Data/Player Detected State"
)]
public class D_PlayerDetectedState : ScriptableObject
{
    public float LongRangeActionTime = 0.3f,
        CloseRangeActionTime = 0.1f;
}
