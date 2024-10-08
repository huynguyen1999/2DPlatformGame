using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArrowController : MonoBehaviour
{
    [SerializeField]
    private Transform _damagePosition;

    [SerializeField]
    private float _arrowDestroyTime = 3f;

    [SerializeField]
    private float _arrowLifeSpan = 10f;

    [SerializeField]
    private float _damageRadius = 0.1f;

    [SerializeField]
    private float _collisionTorqueMin = -10f,
        _collisionTorqueMax = 10f,
        _knockBackXMinForce = 0.5f,
        _knockBackXMaxForce = 1f,
        _knockBackYMinForce = 0.5f,
        _knockBackYMaxForce = 2f;
    private LayerMask _whatIsTarget;

    [SerializeField]
    private LayerMask _whatIsGround;

    private float _travelDistance = 3f,
        _speed = 8f,
        _damage = 0f,
        _xStartPosition = 0f;
    private bool _hasHitGround,
        _hasHitTarget,
        _isGravityOn;
    private Rigidbody2D _rb;
    private PolygonCollider2D _collider;
    private Animator _anim;

    public void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<PolygonCollider2D>();
        _anim = GetComponent<Animator>();
        _hasHitGround = false;
        _hasHitTarget = false;
        _isGravityOn = false;
        StartCoroutine(RemoveArrow(_arrowLifeSpan));
    }

    public void Update()
    {
    }

    public void FixedUpdate()
    {
        if (Mathf.Abs(_xStartPosition - transform.position.x) >= _travelDistance)
        {
            _rb.gravityScale = 5f;
            _isGravityOn = true;
        }
        if (!_hasHitGround && !_hasHitTarget)
        {
            _rb.velocity = new Vector2(_speed * transform.right.x, 0);
        }
        CheckGroundHit();
        CheckTargetHit();
    }

    private void CheckGroundHit()
    {
        if (_hasHitGround)
            return;
        Collider2D groundHit = Physics2D.OverlapCircle(
            _damagePosition.position,
            _damageRadius,
            _whatIsGround
        );
        if (groundHit == null)
            return;
        _rb.velocity = new Vector2(0, 0);
        _rb.angularVelocity = 0f;
        _hasHitGround = true;
        _rb.isKinematic = true;
        _anim.Play("Arrow_Idle");
        StartCoroutine(RemoveArrow(_arrowDestroyTime));
    }

    private void CheckTargetHit()
    {
        if (_hasHitTarget || _hasHitGround)
            return;
        Collider2D[] targetsHit = Physics2D.OverlapCircleAll(
            _damagePosition.position,
            _damageRadius,
            _whatIsTarget
        );
        if (targetsHit == null || targetsHit.Length == 0)
            return;
        KnockBack();
        foreach (Collider2D targetHit in targetsHit)
        {
            IDamageable target = targetHit.GetComponent<IDamageable>();
            if (target == null) continue;
            AttackDetails attackDetails = new(transform, (int)transform.localScale.x, _damage);
            target?.OnHit(attackDetails);
            _hasHitTarget = true;
            _rb.gravityScale = 4f;
            _isGravityOn = true;
            return;
        }
    }

    private void KnockBack()
    {
        _rb.velocity = Vector2.zero;
        _rb.AddForce(
            new Vector2(
                -transform.right.x * Random.Range(_knockBackXMinForce, _knockBackXMaxForce),
                Random.Range(_knockBackYMinForce, _knockBackYMaxForce)
            ),
            ForceMode2D.Impulse
        );
        _rb.AddTorque(Random.Range(_collisionTorqueMin, _collisionTorqueMax), ForceMode2D.Impulse);
    }

    private IEnumerator RemoveArrow(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    public void FireArrow(float speed, float travelDistance, float damage, LayerMask whatIsTarget)
    {
        _travelDistance = travelDistance;
        _damage = damage;
        _whatIsTarget = whatIsTarget;
        _speed = speed;
        _xStartPosition = transform.position.x;
        _anim.Play("Arrow_Flying");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_damagePosition.position, _damageRadius);
    }
}
