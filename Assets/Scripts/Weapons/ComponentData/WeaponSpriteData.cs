using UnityEngine;


public class WeaponSpriteData : ComponentData<AttackSprites>
{
    public WeaponSpriteData()
    {
        ComponentDependency = typeof(WeaponSprite);
    }
}