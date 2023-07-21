using UnityEngine;

[CreateAssetMenu(
    fileName = "newLookForPlayerStateData",
    menuName = "Data/State Data/Look For Player State"
)]
public class D_LookForPlayerState : ScriptableObject
{
    public int AmountOfTurns = 2;
    public float TurnTransitionTime = 0.75f;
}
