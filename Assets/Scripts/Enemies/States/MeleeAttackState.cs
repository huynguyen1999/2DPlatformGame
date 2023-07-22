using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeleeAttackState : AttackState
{
    protected new D_MeleeAttackState _stateData;
    protected bool _isTargetInMinAggroRange,
        _isTargetInMaxAggroRange;

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
        Collider2D[] hitTargets = new Collider2D[1];
        ContactFilter2D contactFilter =
            new() { useLayerMask = true, layerMask = _entity.EntityData.WhatIsTarget };
        Physics2D.OverlapCollider(_entity.AttackTriggerCollider, contactFilter, hitTargets);
        foreach (Collider2D collider in hitTargets)
        {
            IDamageable targetController = collider.GetComponent<IDamageable>();
            AttackDetails attackDetails = new(_entity.AliveGO.transform, _stateData.AttackDamage);
            targetController?.OnHit(attackDetails);
        }
    }
}
