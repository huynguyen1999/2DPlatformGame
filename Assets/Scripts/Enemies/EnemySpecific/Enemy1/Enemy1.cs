using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class Enemy1 : Entity
{
    public E1_IdleState IdleState { get; private set; }
    public E1_MoveState MoveState { get; private set; }

    [SerializeField]
    private D_IdleState _idleStateData;

    [SerializeField]
    private D_MoveState _moveStateData;

    public override void Start()
    {
        base.Start();
        MoveState = new E1_MoveState(this, StateMachine, "move", _moveStateData, this);
        IdleState = new E1_IdleState(this, StateMachine, "idle", _idleStateData, this);
        StateMachine.Initialize(IdleState);
    }
}
