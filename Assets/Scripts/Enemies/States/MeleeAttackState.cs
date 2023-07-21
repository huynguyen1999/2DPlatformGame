using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeleeAttackState : AttackState
{
    protected new D_MeleeAttackState _stateData;
    protected bool _isTargetInMinAggroRange;

    public MeleeAttackState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animBoolName,
        D_MeleeAttackState stateData
    )
        : base(entity, stateMachine, animBoolName, null)
    {
        _stateData = stateData;
    }

    public override void Enter(object data=null)
    {
        base.Enter(data);
        _entity.AttackTriggerCollider.enabled = true;
        TriggerAttack();
    }

    public override void Exit()
    {
        base.Exit();
        _entity.AttackTriggerCollider.enabled = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        _isTargetInMinAggroRange = _entity.CheckTargetInMinAggroRange();
    }

    public override void TriggerAttack()
    {
        // base.TriggerAttack();
        Collider2D[] hitTargets = new Collider2D[1];
        ContactFilter2D contactFilter =
            new() { useLayerMask = true, layerMask = _entity.EntityData.WhatIsTarget };
        Physics2D.OverlapCollider(_entity.AttackTriggerCollider, contactFilter, hitTargets);
        foreach (Collider2D collider in hitTargets)
        {
            IDamageable targetController = collider?.GetComponent<IDamageable>();
            AttackDetails attackDetails =
                new(attackSourceTransform: _entity.transform, damage: _stateData.AttackDamage);
            targetController?.OnHit(attackDetails);
        }
    }
}
