using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SwordAttackState : WeaponBaseState
{
    private int attackCounter = 1;
    private float attackMovementSpeed = 0f;
    public SwordAttackState(WeaponSword context, WeaponData weaponData, Player player, PlayerData playerData, PlayerAttackState playerAttackState, string animBoolName) :
        base(context, weaponData, player, playerData, playerAttackState, animBoolName)
    {
    }
    public override void Enter(object data = null)
    {
        base.Enter(data);
        context.baseAnimator.SetInteger("AttackCounter", attackCounter);
        context.weaponAnimator.SetInteger("AttackCounter", attackCounter);
        player.StartCoroutine(ResetAttackCounter());
        attackMovementSpeed = (attackCounter - 1 < weaponData.movementSpeed.Length) ? weaponData.movementSpeed[attackCounter - 1] : 0f;
    }

    private IEnumerator ResetAttackCounter()
    {
        yield return new WaitForSeconds(weaponData.attackCounterResetTime);
        attackCounter = 1;
    }
    public override void Exit()
    {
        base.Exit();
        attackCounter++;
        if (attackCounter > weaponData.maxAttackCounter)
        {
            attackCounter = 1;
        }
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (CanFlip())
        {
            playerAttackState.CheckIfShouldFlip();
        }
        player.SetVelocityX(attackMovementSpeed * player.FacingDirection);
    }
    public override void CheckSwitchStates()
    {
        if (IsAnimationFinished())
        {
            SwitchState(context.emptyState);
        }
    }
}