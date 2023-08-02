using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private WeaponData weaponData;
    protected Animator baseAnimator,
        weaponAnimator;
    protected float baseAnimationNormTime,
        weaponAnimationNormTime;
    private float lastAttackTime = Mathf.NegativeInfinity;
    private PlayerAttackState state;
    protected int attackCounter;

    protected virtual void Start()
    {
        baseAnimator = transform.Find("Base").GetComponent<Animator>();
        weaponAnimator = transform.Find("Weapon").GetComponent<Animator>();
        gameObject.SetActive(false);
        attackCounter = 0;
    }

    public void InitializeWeapon(PlayerAttackState state)
    {
        this.state = state;
    }

    public virtual void EnterWeapon()
    {
        if (Time.time > lastAttackTime + weaponData.attackCounterResetTime)
        {
            attackCounter = 0;
        }

        if (attackCounter >= 3)
        {
            attackCounter = 0;
        }
        gameObject.SetActive(true);
        baseAnimator.SetBool("Attack", true);
        weaponAnimator.SetBool("Attack", true);
        state.SetVelocity(weaponData.movementSpeed[attackCounter]);
        baseAnimator.SetInteger("AttackCounter", attackCounter);
        weaponAnimator.SetInteger("AttackCounter", attackCounter);
    }

    public virtual void LogicUpdateWeapon()
    {
        baseAnimationNormTime = baseAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        weaponAnimationNormTime = weaponAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    public virtual bool IsAnimationFinished()
    {
        return baseAnimationNormTime >= 1f && weaponAnimationNormTime >= 1f;
    }

    public virtual bool CanFlip()
    {
        return baseAnimationNormTime >= 0.75f && weaponAnimationNormTime >= 0.75f;
    }

    public virtual void ExitWeapon()
    {
        baseAnimator.SetBool("Attack", false);
        weaponAnimator.SetBool("Attack", false);
        attackCounter += 1;
        gameObject.SetActive(false);
        lastAttackTime = Time.time;
    }
}
