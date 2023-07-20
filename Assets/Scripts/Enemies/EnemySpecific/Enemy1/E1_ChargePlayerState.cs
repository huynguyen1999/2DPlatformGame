using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class E1_ChargePlayerState : ChargePlayerState
{
    private Enemy1 _enemy;

    public E1_ChargePlayerState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animBoolName,
        D_ChargePlayerState stateData,
        Enemy1 enemy
    )
        : base(entity, stateMachine, animBoolName, stateData)
    {
        _enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // TODO: _isChargeTimeOver => PlayerDetectedState
        // TODO: !_isPlayerInMinAggroRange => 
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
