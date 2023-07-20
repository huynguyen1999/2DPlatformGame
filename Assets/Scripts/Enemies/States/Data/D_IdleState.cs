using UnityEngine;

[CreateAssetMenu(fileName = "newIdleStateData", menuName = "Data/State Data/Idle State")]
public class D_IdleState : ScriptableObject
{
    public float MinIdleTime = 0.5f;
    public float MaxIdleTime = 2f;
}
