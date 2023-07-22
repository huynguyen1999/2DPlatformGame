using UnityEngine;

[CreateAssetMenu(fileName = "newDodgeStateData", menuName = "Data/State Data/Dodge State")]
public class D_DodgeState : ScriptableObject
{
    public float DodgeCoolDown = 5f;
    public float DodgeDuration = 0.5f;
    public Vector2 DodgeJumpForce = new(10f, 10f);
}
