using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RangedAttackState : AttackState
{
    protected new D_RangedAttackState _stateData;
    protected bool _isTargetInMinAggroRange,
        _isTargetInMaxAggroRange,
        _hasShotProjectile;

    public RangedAttackState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animName,
        D_RangedAttackState stateData
    )
        : base(entity, stateMachine, animName, null)
    {
        _stateData = stateData;
    }

    public override void Enter(object data = null)
    {
        base.Enter(data);
        _isTargetInMinAggroRange = _entity.CheckTargetInMinAggroRange();
        _isTargetInMaxAggroRange = _entity.CheckTargetInMaxAggroRange();
        _hasShotProjectile = false;
    }

    public override void Exit()
    {
        base.Exit();
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
        if (
            _entity.Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f
            && _hasShotProjectile == false
        )
        {
            TriggerAttack();
        }
    }

    public override void TriggerAttack()
    {
        _hasShotProjectile = true;
        GameObject arrow = Object.Instantiate(
            _stateData.Projectile,
            _entity.ProjectileStart.transform.position,
            _entity.AliveGO.transform.rotation
        );
        ArrowController arrowController = arrow.GetComponent<ArrowController>();
        if (!arrowController)
            return;
        arrowController.FireArrow(
            _stateData.ProjectileSpeed,
            _stateData.ProjectileTravelDistance,
            _stateData.ProjectileDamage,
            _entity.EntityData.WhatIsTarget
        );
    }
}
