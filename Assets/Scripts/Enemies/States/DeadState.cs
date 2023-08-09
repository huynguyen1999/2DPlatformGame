using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeadState : State
{
    protected D_DeadState _stateData;

    public DeadState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animName,
        D_DeadState stateData
    )
        : base(entity, stateMachine, animName)
    {
        _stateData = stateData;
    }

    public override void Enter(object data = null)
    {
        base.Enter(data);
        _core.Movement.SetVelocityX(0f);
        Object.Instantiate(
            _stateData.DeathBloodParticle,
            _entity.transform.position,
            _entity.transform.rotation
        );
        Object.Instantiate(
            _stateData.DeathChunkParticle,
            _entity.transform.position,
            _entity.transform.rotation
        );
        Object.Destroy(_entity.gameObject);
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
    }
}
