using UnityEngine;


public abstract class WeaponComponent : MonoBehaviour
{
    protected bool isAttackActive;
    protected Weapon weapon;
    protected virtual void Awake()
    {
        weapon = GetComponent<Weapon>();
    }
    public virtual void Init() { }
    protected virtual void HandleEnter()
    {
        isAttackActive = true;
    }

    protected virtual void HandleExit()
    {
        isAttackActive = false;
    }

    protected virtual void Start()
    {
        weapon.OnEnter += HandleEnter;
        weapon.OnExit += HandleExit;
    }
    protected virtual void OnDestroy()
    {
        weapon.OnEnter -= HandleEnter;
        weapon.OnExit -= HandleExit;
    }
}
/// <summary>
/// Weapon Component base class, subscribe to the weapon on enter or exit event
/// Note: there will be multiple components as subscriber to the weapon class
/// </summary>
/// <typeparam name="T1">ComponentData</typeparam>
/// <typeparam name="T2">AttackData</typeparam>
public abstract class WeaponComponent<T1, T2> : WeaponComponent where T1 : ComponentData<T2> where T2 : AttackData
{

    protected T1 data;
    protected T2 currentAttackData;


    public override void Init()
    {
        base.Init();
        data = weapon.Data.GetData<T1>();
    }
    protected override void HandleEnter()
    {
        base.HandleEnter();
        currentAttackData = data.AttackData[weapon.CurrentAttackCounter];
    }
}