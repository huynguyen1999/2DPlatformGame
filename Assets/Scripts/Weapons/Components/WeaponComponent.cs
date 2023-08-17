using UnityEngine;

public abstract class WeaponComponent<T1, T2> : MonoBehaviour where T1 : ComponentData<T2> where T2 : AttackData
{
    protected Weapon weapon;
    protected T1 data;
    protected T2 currentAttackData;
    protected bool isAttackActive;

    protected virtual void Awake()
    {
        weapon = GetComponent<Weapon>();
        data = weapon.Data.GetData<T1>();
    }
    protected virtual void HandleEnter()
    {
        isAttackActive = true;
        currentAttackData = data.AttackData[weapon.CurrentAttackCounter];
    }

    protected virtual void HandleExit()
    {
        isAttackActive = false;
    }

    protected virtual void OnEnable()
    {
        weapon.OnEnter += HandleEnter;
        weapon.OnExit += HandleExit;
    }

    protected virtual void OnDisable()
    {
        weapon.OnEnter -= HandleEnter;
        weapon.OnExit -= HandleExit;
    }
}