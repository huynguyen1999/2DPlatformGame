using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public abstract class WeaponBaseState
{
    protected Weapon context;
    protected WeaponData weaponData;
    protected Player player;
    protected PlayerData playerData;
    protected PlayerAttackState playerAttackState;
    private string animBoolName;
    protected float baseAnimationNormTime,
        weaponAnimationNormTime;
    protected float startTime;

    public WeaponBaseState(
        Weapon context,
        WeaponData weaponData,
        Player player,
        PlayerData playerData,
        PlayerAttackState playerAttackState,
        string animBoolName
    )
    {
        this.context = context;
        this.weaponData = weaponData;
        this.player = player;
        this.playerData = playerData;
        this.playerAttackState = playerAttackState;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter(object data = null)
    {
        startTime = Time.time;
        context.baseAnimator.SetBool(animBoolName, true);
        context.weaponAnimator.SetBool(animBoolName, true);
        baseAnimationNormTime = 0f;
        weaponAnimationNormTime = 0f;
    }

    public virtual void Exit()
    {
        context.baseAnimator.SetBool(animBoolName, false);
        context.weaponAnimator.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {
        CheckSwitchStates();
        baseAnimationNormTime = context.baseAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        weaponAnimationNormTime = context.weaponAnimator
            .GetCurrentAnimatorStateInfo(0)
            .normalizedTime;
    }

    public virtual void PhysicsUpdate() { }

    public abstract void CheckSwitchStates();

    protected void SwitchState(WeaponBaseState newState)
    {
        if (newState == null)
            return;
        Exit();
        context.previousState = context.currentState;
        context.currentState = newState;
        newState.Enter();
    }

    public bool IsAnimationFinished()
    {
        return baseAnimationNormTime >= 1f && weaponAnimationNormTime >= 1f;
    }

    public bool CanFlip()
    {
        return baseAnimationNormTime >= 0.75f && weaponAnimationNormTime >= 0.75f;
    }
}
