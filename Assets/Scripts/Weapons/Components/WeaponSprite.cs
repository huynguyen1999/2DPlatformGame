using UnityEngine;
using System;

public class WeaponSprite : WeaponComponent<WeaponSpriteData, AttackSprites>
{
    protected SpriteRenderer baseSpriteRenderer;
    protected SpriteRenderer weaponSpriteRenderer;
    private int currentWeaponSpriteIndex;
    protected override void Awake()
    {
        base.Awake();
        baseSpriteRenderer = weapon.BaseGameObject.GetComponent<SpriteRenderer>();
        weaponSpriteRenderer = weapon.WeaponSpriteGameObject.GetComponent<SpriteRenderer>();
        data = weapon.Data.GetData<WeaponSpriteData>();
    }
    protected override void HandleEnter()
    {
        base.HandleEnter();
        currentWeaponSpriteIndex = 0;
    }
    private void HandleSpriteChange(SpriteRenderer sr)
    {
        if (!isAttackActive)
        {
            weaponSpriteRenderer.sprite = null;
            return;
        }

        var currentAttackSprites = currentAttackData.Sprites;

        if (currentWeaponSpriteIndex >= currentAttackSprites.Length)
        {
            Debug.LogWarning($"{weapon.name} weapon sprites length mismatch");
            return;
        }

        weaponSpriteRenderer.sprite = currentAttackSprites[currentWeaponSpriteIndex];
        currentWeaponSpriteIndex++;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        baseSpriteRenderer.RegisterSpriteChangeCallback(HandleSpriteChange);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        baseSpriteRenderer.UnregisterSpriteChangeCallback(HandleSpriteChange);
    }
}