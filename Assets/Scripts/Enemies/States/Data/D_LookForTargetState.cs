using UnityEngine;

[CreateAssetMenu(
    fileName = "newLookForTargetStateData",
    menuName = "Data/State Data/Look For Target State"
)]
public class D_LookForTargetState : ScriptableObject
{
    public int AmountOfTurns = 2;
    public float TurnTransitionTime = 0.5f;
}
