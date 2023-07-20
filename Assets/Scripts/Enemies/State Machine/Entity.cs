using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine StateMachine;
    public D_Entity EntityData;
    public Rigidbody2D RB { get; private set; }
    public Animator Anim { get; private set; }
    public GameObject AliveGO { get; private set; }
    public int FacingDirection { get; private set; }

    private Vector2 _velocityWorkspace;

    [SerializeField]
    private Transform _wallCheck;

    [SerializeField]
    private Transform _ledgeCheck;

    public virtual void Start()
    {
        AliveGO = transform.Find("Alive").gameObject;
        RB = AliveGO.GetComponent<Rigidbody2D>();
        Anim = AliveGO.GetComponent<Animator>();
        StateMachine = new FiniteStateMachine();
        FacingDirection = 1;
    }

    public virtual void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        _velocityWorkspace.Set(FacingDirection * velocity, RB.velocity.y);
        RB.velocity = _velocityWorkspace;
    }

    public virtual bool CheckWall()
    {
        return Physics2D
                .Raycast(
                    _wallCheck.position,
                    AliveGO.transform.right,
                    EntityData.WallCheckDistance,
                    EntityData.WhatIsGround
                )
                .collider != null;
    }

    public virtual bool CheckLedge()
    {
        return Physics2D
                .Raycast(
                    _ledgeCheck.position,
                    Vector2.down,
                    EntityData.LedgeCheckDistance,
                    EntityData.WhatIsGround
                )
                .collider == null;
    }

    public virtual void Flip()
    {
        FacingDirection *= -1;
        AliveGO.transform.Rotate(0f, 180f, 0f);
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(
            _wallCheck.position,
            _wallCheck.position
                + (Vector3)(Vector2.right * FacingDirection * EntityData.WallCheckDistance)
        );
        Gizmos.DrawLine(
            _ledgeCheck.position,
            _ledgeCheck.position + (Vector3)(Vector2.down * EntityData.LedgeCheckDistance)
        );
    }
}
