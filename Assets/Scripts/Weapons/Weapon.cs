using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Weapon : MonoBehaviour
{
    [field: SerializeField] public WeaponDataSO Data { get; private set; }
    [SerializeField] private float attackCounterResetCooldown;

    public int CurrentAttackCounter
    {
        get => currentAttackCounter;
        private set => currentAttackCounter = value >= Data.NumberOfAttacks ? 0 : value;
    }

    public event Action OnEnter;
    public event Action OnExit;
    public Core Core { get; private set; }
    private Animator anim;
    public GameObject BaseGameObject { get; private set; }
    public GameObject WeaponSpriteGameObject { get; private set; }
    public AnimationEventHandler EventHandler { get; private set; }

    private int currentAttackCounter;

    private Timer attackCounterResetTimer;

    private void Awake()
    {
        BaseGameObject = transform.Find("Base").gameObject;
        WeaponSpriteGameObject = transform.Find("WeaponSprite").gameObject;
        anim = BaseGameObject.GetComponent<Animator>();
        EventHandler = BaseGameObject.GetComponent<AnimationEventHandler>();
        attackCounterResetTimer = new Timer(attackCounterResetCooldown);
    }
    public void SetCore(Core core) { this.Core = core; }
    public void Enter()
    {
        attackCounterResetTimer.StopTimer();

        anim.SetBool("Active", true);
        anim.SetInteger("AttackCounter", currentAttackCounter);

        OnEnter?.Invoke();
    }

    private void Exit()
    {
        anim.SetBool("Active", false);
        CurrentAttackCounter++;
        attackCounterResetTimer.StartTimer();
        OnExit?.Invoke();
    }

    private void Update()
    {
        attackCounterResetTimer.Tick();
    }

    private void ResetAttackCounter()
    {
        CurrentAttackCounter = 0;
    }

    private void OnEnable()
    {
        EventHandler.OnFinish += Exit;
        attackCounterResetTimer.OnTimerFinished += ResetAttackCounter;
    }

    private void OnDisable()
    {
        EventHandler.OnFinish -= Exit;
        attackCounterResetTimer.OnTimerFinished -= ResetAttackCounter;
    }
}