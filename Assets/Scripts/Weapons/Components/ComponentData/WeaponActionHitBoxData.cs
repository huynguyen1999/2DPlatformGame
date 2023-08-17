using UnityEngine;

public class WeaponActionHitBoxData : ComponentData<AttackActionHitBox>
{
    [field: SerializeField]
    public LayerMask DetectableLayers { get; private set; }
}