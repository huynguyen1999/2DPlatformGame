using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class WeaponSprite : WeaponComponent<WeaponSpriteData, AttackSprites>
{
    protected SpriteRenderer baseSpriteRenderer;
    protected SpriteRenderer weaponSpriteRenderer;
    private int currentWeaponSpriteIndex;
    private Sprite[] currentPhaseSprites;
    protected override void Start()
    {
        base.Start();
        baseSpriteRenderer = weapon.BaseGameObject.GetComponent<SpriteRenderer>();
        weaponSpriteRenderer = weapon.WeaponSpriteGameObject.GetComponent<SpriteRenderer>();
        baseSpriteRenderer.RegisterSpriteChangeCallback(HandleSpriteChange);
        weapon.EventHandler.OnEnterAttackPhase += HandleEnterAttackPhase;
    }
    protected override void HandleEnter()
    {
        base.HandleEnter();
        currentWeaponSpriteIndex = 0;
    }

    private void HandleEnterAttackPhase(AttackPhases phase)
    {
        currentWeaponSpriteIndex = 0;
        currentPhaseSprites = currentAttackData.PhaseSprites.FirstOrDefault(data => data.Phase == phase).Sprites;
        weaponSpriteRenderer.sprite = currentPhaseSprites[currentWeaponSpriteIndex];
    }
    private void HandleSpriteChange(SpriteRenderer sr)
    {
        if (!isAttackActive)
        {
            weaponSpriteRenderer.sprite = null;
            return;
        }

        if (currentWeaponSpriteIndex >= currentPhaseSprites.Length)
        {
            return;
        }
        weaponSpriteRenderer.sprite = currentPhaseSprites[currentWeaponSpriteIndex];
        currentWeaponSpriteIndex++;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        baseSpriteRenderer.UnregisterSpriteChangeCallback(HandleSpriteChange);
        weapon.EventHandler.OnEnterAttackPhase -= HandleEnterAttackPhase;
    }
}