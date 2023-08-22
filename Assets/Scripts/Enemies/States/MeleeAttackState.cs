using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeleeAttackState : AttackState
{
    protected new D_MeleeAttackState _stateData;
    protected bool _isTargetInMinAggroRange,
        _isTargetInMaxAggroRange;
    private List<int> detectedDamageableInstanceIDs = new();

    public MeleeAttackState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animName,
        D_MeleeAttackState stateData
    )
        : base(entity, stateMachine, animName, null)
    {
        _stateData = stateData;
    }

    public override void Enter(object data = null)
    {
        base.Enter(data);
        _entity.AttackTriggerCollider.enabled = true;
        detectedDamageableInstanceIDs = new();
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
        _isTargetInMaxAggroRange = _entity.CheckTargetInMaxAggroRange();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
        List<Collider2D> hitTargets = new();
        ContactFilter2D contactFilter =
            new() { useLayerMask = true, layerMask = _entity.EntityData.WhatIsTarget };
        Physics2D.OverlapCollider(_entity.AttackTriggerCollider, contactFilter, hitTargets);
        foreach (Collider2D item in hitTargets)
        {
            if (item.TryGetComponent(out IDamageable damageable))
            {
                if (damageable == null || detectedDamageableInstanceIDs.Contains(item.GetInstanceID()))
                {
                    continue;
                }
                detectedDamageableInstanceIDs.Add(item.GetInstanceID());
                AttackDetails attackDetails = new(_entity.transform, _core.Movement.FacingDirection, _stateData.AttackDamage);
                damageable.OnHit(attackDetails);
            }
            if (item.TryGetComponent(out IKnockbackable knockbackable))
            {
                knockbackable.KnockBack(_stateData.KnockbackAngle, _stateData.KnockbackForce, _core.Movement.FacingDirection);
            }
        }
    }
}
