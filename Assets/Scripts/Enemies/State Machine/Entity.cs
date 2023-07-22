using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    public FiniteStateMachine StateMachine;
    public D_Entity EntityData;
    public Rigidbody2D RB { get; private set; }
    public PolygonCollider2D AttackTriggerCollider { get; private set; }
    public Animator Anim { get; private set; }
    public GameObject AliveGO { get; private set; }
    public int FacingDirection { get; private set; }

    private Vector2 _velocityWorkspace;

    [SerializeField]
    private Transform _wallCheck;

    [SerializeField]
    private Transform _ledgeCheck;

    [SerializeField]
    private Transform _targetCheck;

    public Transform ProjectileStart;

    protected float _currentHealth;

    public virtual void Start()
    {
        AliveGO = transform.Find("Alive").gameObject;
        RB = AliveGO.GetComponent<Rigidbody2D>();
        Anim = AliveGO.GetComponent<Animator>();
        AttackTriggerCollider = AliveGO.GetComponent<PolygonCollider2D>();
        AttackTriggerCollider.enabled = false;
        StateMachine = new FiniteStateMachine();
        _currentHealth = EntityData.MaxHealth;
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

    public virtual void SetXVelocity(float xVelocity)
    {
        _velocityWorkspace.Set(FacingDirection * xVelocity, RB.velocity.y);
        RB.velocity = _velocityWorkspace;
    }

    public virtual void SetVelocity(Vector2 velocity)
    {
        RB.velocity = velocity;
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
                .collider != null;
    }

    public virtual bool CheckTargetInMinAggroRange()
    {
        return Physics2D
                .Raycast(
                    _targetCheck.position,
                    AliveGO.transform.right,
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
                    AliveGO.transform.right,
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
                    AliveGO.transform.right,
                    EntityData.CloseRangeActionDistance,
                    EntityData.WhatIsTarget
                )
                .collider != null;
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
            _wallCheck.position + (Vector3)(Vector2.right * EntityData.WallCheckDistance)
        );
        Gizmos.DrawLine(
            _ledgeCheck.position,
            _ledgeCheck.position + (Vector3)(Vector2.down * EntityData.LedgeCheckDistance)
        );
    }

    public virtual void OnHit(AttackDetails attackDetails)
    {
        Vector2 attackDirection = attackDetails.AttackSourceTransform.right;
        SetVelocity(
            new Vector2(
                attackDirection.x * EntityData.KnockBackForce.x,
                EntityData.KnockBackForce.y
            )
        );
        Instantiate(
            EntityData.HitParticle,
            AliveGO.transform.position,
            Quaternion.Euler(0f, 0f, Random.Range(0f, 360f))
        );
        _currentHealth -= attackDetails.Damage;
    }
}
