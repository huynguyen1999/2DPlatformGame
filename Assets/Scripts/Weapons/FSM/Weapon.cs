using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    protected WeaponData weaponData;
    protected Player player;
    protected PlayerData playerData;
    protected PlayerAttackState playerAttackState;
    public Animator baseAnimator { get; private set; }
    public Animator weaponAnimator { get; private set; }
    protected float lastAttackTime = Mathf.NegativeInfinity;
    public WeaponEmptyState emptyState;

    public WeaponBaseState currentState,
        previousState;

    protected virtual void Start()
    {
        baseAnimator = transform.Find("Base").GetComponent<Animator>();
        weaponAnimator = transform.Find("Weapon").GetComponent<Animator>();
        gameObject.SetActive(false);
    }

    public virtual void InitializeWeapon(Player player, PlayerData playerData, PlayerAttackState playerAttackState)
    {
        this.player = player;
        this.playerData = playerData;
        this.playerAttackState = playerAttackState;
        emptyState = new WeaponEmptyState(this, weaponData, player, playerData, playerAttackState, "Empty");
    }

    public virtual void UseWeapon()
    {
        gameObject.SetActive(true);
        baseAnimator.SetBool("Attack", true);
        weaponAnimator.SetBool("Attack", true);
    }

    public virtual void Update()
    {
        if (currentState != null)
            currentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        if (currentState != null)
            currentState.PhysicsUpdate();
    }

    public virtual void UnuseWeapon()
    {
        baseAnimator.SetBool("Attack", false);
        weaponAnimator.SetBool("Attack", false);
        gameObject.SetActive(false);
    }
}
