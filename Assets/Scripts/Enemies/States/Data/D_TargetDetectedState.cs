using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "newTargetDetectedStateData",
    menuName = "Data/State Data/Target Detected State"
)]
public class D_TargetDetectedState : ScriptableObject
{
    public float LongRangeActionTime = 0.3f,
        CloseRangeActionTime = 0.1f;
}
