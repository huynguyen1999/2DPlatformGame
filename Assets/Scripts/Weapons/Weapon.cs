using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Weapon : MonoBehaviour
{
    public event Action<bool> OnCurrentInputChange;
    public WeaponDataSO Data { get; private set; }
    public int CurrentAttackCounter
    {
        get => currentAttackCounter;
        private set => currentAttackCounter = value >= Data.NumberOfAttacks ? 0 : value;
    }
    public bool CurrentInput
    {
        get => currentInput;
        set
        {
            if (currentInput != value)
            {
                currentInput = value;
                OnCurrentInputChange?.Invoke(currentInput);
            }
        }
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
    private bool currentInput;
    private void Awake()
    {
        BaseGameObject = transform.Find("Base").gameObject;
        WeaponSpriteGameObject = transform.Find("WeaponSprite").gameObject;
        anim = BaseGameObject.GetComponent<Animator>();
        EventHandler = BaseGameObject.GetComponent<AnimationEventHandler>();
    }
    public void SetCore(Core core)
    {
        this.Core = core;
    }
    public void SetData(WeaponDataSO data)
    {
        this.Data = data;
        attackCounterResetTimer = new Timer(Data.AttackCounterResetCooldown);

    }
    public void Enter()
    {
        attackCounterResetTimer.StopTimer();
        anim.SetBool("Active", true);
        anim.SetInteger("AttackCounter", currentAttackCounter);
        EventHandler.OnFinish += Exit;
        attackCounterResetTimer.OnTimerFinished += ResetAttackCounter;
        OnEnter?.Invoke();
    }

    private void Exit()
    {
        anim.SetBool("Active", false);
        CurrentAttackCounter++;
        attackCounterResetTimer.StartTimer();
        EventHandler.OnFinish -= Exit;
        attackCounterResetTimer.OnTimerFinished -= ResetAttackCounter;
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
}