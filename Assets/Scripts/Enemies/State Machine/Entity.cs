using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Core Core { get; private set; }
    public FiniteStateMachine StateMachine;
    public D_Entity EntityData;
    public PolygonCollider2D AttackTriggerCollider { get; private set; }
    public Animator Anim { get; private set; }

    [SerializeField]
    private Transform _targetCheck;

    public Transform ProjectileStart;

    public virtual void Awake()
    {
        Core = GetComponentInChildren<Core>();
    }
    public virtual void Start()
    {
        Anim = GetComponent<Animator>();
        AttackTriggerCollider = GetComponent<PolygonCollider2D>();
        AttackTriggerCollider.enabled = false;
        StateMachine = new FiniteStateMachine();
        Core.Stats.Poise.OnCurrentValueZero += HandlePoiseZero;
    }
    public virtual void OnDestroy()
    {
        Core.Stats.Poise.OnCurrentValueZero -= HandlePoiseZero;
    }
    protected virtual void HandlePoiseZero() { }
    public virtual void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public virtual bool CheckTargetInMinAggroRange()
    {
        return Physics2D
                .Raycast(
                    _targetCheck.position,
                    transform.right,
                    EntityData.MinAggroDistance,
                    EntityData.WhatIsTarget
                )
                .collider != null;
        ;
    }

    public virtual bool CheckTargetInMaxAggroRange()
    {
        return Physics2D
                .Raycast(
                    _targetCheck.position,
                    transform.right,
                    EntityData.MaxAggroDistance,
                    EntityData.WhatIsTarget
                )
                .collider != null;
        ;
    }

    public virtual bool CheckTargetInCloseRangeAction()
    {
        return Physics2D
                .Raycast(
                    _targetCheck.position,
                    transform.right,
                    EntityData.CloseRangeActionDistance,
                    EntityData.WhatIsTarget
                )
                .collider != null;
    }
}
