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

    private float _travelDistance = 0f,
        _speed = 0f,
        _damage = 0f,
        _xStartPosition = 0f;
    private bool _hasHitGround,
        _hasHitTarget;
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
        StartCoroutine(RemoveArrow(_arrowLifeSpan));
    }

    public void Update() { }

    public void FixedUpdate()
    {
        if (Mathf.Abs(_xStartPosition - transform.position.x) >= _travelDistance)
        {
            _rb.gravityScale = 4;
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
        _rb.isKinematic = true;
        _hasHitGround = true;
        _anim.StopPlayback();
        StartCoroutine(RemoveArrow(_arrowDestroyTime));
    }

    private void CheckTargetHit()
    {
        if (_hasHitTarget)
            return;
        Collider2D targetHit = Physics2D.OverlapCircle(
            _damagePosition.position,
            _damageRadius,
            _whatIsTarget
        );
        if (targetHit == null)
            return;
        KnockBack();
        IDamageable target = targetHit.GetComponent<IDamageable>();
        AttackDetails attackDetails = new(transform, _damage);
        target?.OnHit(attackDetails);
        _hasHitTarget = true;
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
        _rb.gravityScale = 0;
        _xStartPosition = transform.position.x;
        _anim.Play("Arrow_Flying");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_damagePosition.position, _damageRadius);
    }
}
