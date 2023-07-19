using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyInterfaces;

public class BasicEnemyController : MonoBehaviour, IDamageable
{
    private enum State
    {
        Moving,
        KnockBack,
        Dead
    }

    public float RayCastDistance;
    public float MovementSpeed;
    public float MaxHealth;
    public float KnockBackDuration;

    public float TouchDamageDelay,
        TouchDamage,
        TouchDamageWidth,
        TouchDamageHeight;
    public Vector2 KnockBackDirection;
    public Transform EdgeCheck,
        TouchDamageCheck;
    public LayerMask WhatIsGround,
        WhatIsPlayer;
    public GameObject HitParticle,
        DeathChunkParticle,
        DeathBloodParticle;
    private GameObject _aliveGO;
    private Rigidbody2D _aliveRb;
    private BoxCollider2D _aliveCollider;
    private Animator _aliveAnimator;
    private State _currentState;
    private float _facingDirection = 1;
    private float _currentHealth;
    private bool _isGrounded;
    private bool _isAgainstWall;
    private bool _canDealTouchDamage = true;
    private float _damageDirection;
    private Vector2 _touchDamageBotLeft,
        _touchDamageTopRight;

    private void Start()
    {
        _aliveGO = transform.Find("Alive").gameObject;
        _aliveCollider = _aliveGO.GetComponent<BoxCollider2D>();
        _aliveRb = _aliveGO.GetComponent<Rigidbody2D>();
        _aliveAnimator = _aliveGO.GetComponent<Animator>();
        _currentHealth = MaxHealth;
        KnockBackDirection.Normalize();
        SwitchState(State.Moving);
    }

    private void Update()
    {
        switch (_currentState)
        {
            case State.Moving:
                UpdateMovingState();
                break;
            case State.KnockBack:
                UpdateKnockBackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
    }

    private void SwitchState(State state)
    {
        switch (_currentState)
        {
            case State.Moving:
                ExitMovingState();
                break;
            case State.KnockBack:
                ExitKnockBackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }
        switch (state)
        {
            case State.Moving:
                EnterMovingState();
                break;
            case State.KnockBack:
                EnterKnockBackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }
        _currentState = state;
    }

    public void OnAttack(Transform playerTransform, float damage)
    {
        float dotProduct = Vector2.Dot(playerTransform.right, _aliveGO.transform.right);
        bool isFromBehind = dotProduct > 0f;
        _damageDirection = isFromBehind ? _aliveGO.transform.right.x : -_aliveGO.transform.right.x;
        if (isFromBehind)
        {
            damage *= 2;
        }
        _currentHealth -= damage;
        Instantiate(
            HitParticle,
            _aliveGO.transform.position,
            Quaternion.Euler(0f, 0f, Random.Range(0f, 360f))
        );
        if (_currentHealth <= 0f)
        {
            SwitchState(State.Dead);
        }
        else
        {
            SwitchState(State.KnockBack);
        }
    }

    public void OnTouchDamage(Transform player, float damage) { }

    private void CheckTouchDamage()
    {
        if (!_canDealTouchDamage)
            return;

        _touchDamageBotLeft.Set(
            TouchDamageCheck.position.x - (TouchDamageWidth / 2),
            TouchDamageCheck.position.y - (TouchDamageHeight / 2)
        );
        _touchDamageTopRight.Set(
            TouchDamageCheck.position.x + (TouchDamageWidth / 2),
            TouchDamageCheck.position.y + (TouchDamageHeight / 2)
        );
        Collider2D hit = Physics2D.OverlapArea(
            _touchDamageBotLeft,
            _touchDamageTopRight,
            WhatIsPlayer
        );
        if (hit != null)
        {
            IDamageable player = hit.gameObject.GetComponent<IDamageable>();
            player?.OnTouchDamage(_aliveGO.transform, TouchDamage);
            StartCoroutine(HandleTouchDamageCooldown());
        }
    }

    private IEnumerator HandleTouchDamageCooldown()
    {
        _canDealTouchDamage = false;
        yield return new WaitForSeconds(TouchDamageDelay);
        _canDealTouchDamage = true;
    }

    // Moving state
    private void EnterMovingState() { }

    private void UpdateMovingState()
    {
        _isGrounded =
            Physics2D
                .Raycast(
                    EdgeCheck.position,
                    -_aliveGO.transform.up,
                    _aliveCollider.bounds.extents.y + RayCastDistance,
                    WhatIsGround
                )
                .collider != null;

        _isAgainstWall =
            Physics2D
                .Raycast(
                    EdgeCheck.position,
                    _aliveGO.transform.right,
                    RayCastDistance,
                    WhatIsGround
                )
                .collider != null;
        if (!_isGrounded || _isAgainstWall)
        {
            Flip();
        }
        else
        {
            _aliveRb.velocity = new Vector2(_facingDirection * MovementSpeed, _aliveRb.velocity.y);
        }
        CheckTouchDamage();
    }

    private void Flip()
    {
        _facingDirection *= -1;
        _aliveGO.transform.Rotate(0f, 180f, 0f);
    }

    private void ExitMovingState()
    {
        _aliveRb.velocity = Vector2.zero;
    }

    // knock back state
    private void EnterKnockBackState()
    {
        _aliveRb.velocity = new Vector2(
            KnockBackDirection.x * MovementSpeed * _damageDirection,
            KnockBackDirection.y * MovementSpeed
        );
        _aliveAnimator.SetBool("KnockBack", true);
        StartCoroutine(StopKnockBack());
    }

    private void UpdateKnockBackState() { }

    private IEnumerator StopKnockBack()
    {
        yield return new WaitForSeconds(KnockBackDuration);
        if (_currentState == State.KnockBack)
        {
            _aliveRb.velocity = new Vector2(0f, _aliveRb.velocity.y);
            SwitchState(State.Moving);
        }
    }

    private void ExitKnockBackState()
    {
        _aliveAnimator.SetBool("KnockBack", false);
    }

    // dead state
    private void EnterDeadState()
    {
        Instantiate(
            DeathChunkParticle,
            _aliveGO.transform.position,
            DeathChunkParticle.transform.rotation
        );
        Instantiate(
            DeathBloodParticle,
            _aliveGO.transform.position,
            DeathBloodParticle.transform.rotation
        );
        Destroy(_aliveGO);
        Destroy(gameObject);
    }

    private void UpdateDeadState() { }

    private void ExitDeadState() { }
}
