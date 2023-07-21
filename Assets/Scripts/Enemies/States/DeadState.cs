using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeadState : State
{
    protected D_DeadState _stateData;

    public DeadState(
        Entity entity,
        FiniteStateMachine stateMachine,
        string animBoolName,
        D_DeadState stateData
    )
        : base(entity, stateMachine, animBoolName)
    {
        _stateData = stateData;
    }

    public override void Enter(object data = null)
    {
        base.Enter(data);
        _entity.SetXVelocity(0f);
        Object.Instantiate(
            _stateData.DeathBloodParticle,
            _entity.AliveGO.transform.position,
            _entity.AliveGO.transform.rotation
        );
        Object.Instantiate(
            _stateData.DeathChunkParticle,
            _entity.AliveGO.transform.position,
            _entity.AliveGO.transform.rotation
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
