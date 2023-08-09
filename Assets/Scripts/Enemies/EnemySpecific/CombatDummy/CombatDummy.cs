using UnityEngine;
using System.Collections;
using System.Collections.Generic;

enum CombatDummyAnimation
{
    IsLeftHit,
    Damaged
}

public class CombatDummy : MonoBehaviour, IDamageable
{
    [SerializeField]
    private GameObject hitParticles;
    private Animator anim;
    private Rigidbody2D rb;
    public bool IsHit { get; set; }

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void KnockBack(int direction)
    {
        rb.AddForce(new Vector2(5f * direction, 5f), ForceMode2D.Impulse);
    }
    public void OnHit(AttackDetails attackDetails)
    {
        KnockBack(attackDetails.AttackDirection);
        anim.SetTrigger("Damaged");
        anim.SetBool("IsLeftHit", attackDetails.AttackDirection == -1);
        Instantiate(hitParticles, transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
    }
    #region Old
    // public float MaxHealth;

    // public bool IsKnockBackApplied;
    // public GameObject GOAlive,
    //     GOBrokenTop,
    //     GOBrokenBottom,
    //     HitParticle;
    // public Vector2 KnockBackForce;
    // public float KnockBackDuration;
    // public float DeathTorque;
    // private Rigidbody2D _rbAlive,
    //     _rbBrokenTop,
    //     _rbBrokenBottom;
    // private Animator _animatorAlive;
    // private float _currentHealth;
    // private bool _isKnockingBack;

    // private void Start()
    // {
    //     _currentHealth = MaxHealth;
    //     GOAlive.SetActive(true);
    //     GOBrokenTop.SetActive(false);
    //     GOBrokenBottom.SetActive(false);
    //     _rbAlive = GOAlive.GetComponent<Rigidbody2D>();
    //     _animatorAlive = GOAlive.GetComponent<Animator>();
    //     _rbBrokenTop = GOBrokenTop.GetComponent<Rigidbody2D>();
    //     _rbBrokenBottom = GOBrokenBottom.GetComponent<Rigidbody2D>();
    //     KnockBackForce.Normalize();
    // }

    // private void Update() { }

    // public void OnAttack(Transform playerTransform, float damage)
    // {
    //     float dotProduct = Vector2.Dot(playerTransform.right, transform.right);
    //     bool isLeftHit = dotProduct > 0f;
    //     _animatorAlive.SetBool(CombatDummyAnimation.IsLeftHit.ToString(), isLeftHit);
    //     _animatorAlive.SetTrigger(CombatDummyAnimation.Damaged.ToString());
    //     if (isLeftHit)
    //     { // attack behind
    //         damage *= 2;
    //     }
    //     _currentHealth -= damage;
    //     Instantiate(
    //         HitParticle,
    //         GOAlive.transform.position,
    //         Quaternion.Euler(0f, 0f, Random.Range(0f, 360f))
    //     );
    //     if (_currentHealth <= 0f)
    //     {
    //         Die(playerTransform, damage);
    //     }
    //     else if (IsKnockBackApplied)
    //     {
    //         KnockBack(playerTransform, damage);
    //     }
    // }

    // private void Die(Transform playerTransform, float damage)
    // {
    //     GOAlive.SetActive(false);
    //     GOBrokenBottom.SetActive(true);
    //     GOBrokenTop.SetActive(true);
    //     GOBrokenTop.transform.position = GOAlive.transform.position;
    //     GOBrokenBottom.transform.position = GOAlive.transform.position;
    //     _rbBrokenTop.velocity = new Vector2(
    //         damage * KnockBackForce.x * playerTransform.right.x,
    //         damage * KnockBackForce.y
    //     );
    //     _rbBrokenTop.AddTorque(DeathTorque * playerTransform.right.x, ForceMode2D.Impulse);
    //     _rbBrokenBottom.velocity = new Vector2(
    //         damage * KnockBackForce.x * playerTransform.right.x,
    //         damage * KnockBackForce.y
    //     );
    // }

    // private void KnockBack(Transform playerTransform, float damage)
    // {
    //     _isKnockingBack = true;
    //     _rbAlive.velocity = new Vector2(
    //         damage * KnockBackForce.x * playerTransform.right.x,
    //         damage * KnockBackForce.y
    //     );
    //     StartCoroutine(StopKnockBack());
    // }

    // private IEnumerator StopKnockBack()
    // {
    //     yield return new WaitForSeconds(KnockBackDuration);
    //     _isKnockingBack = false;
    //     _rbAlive.velocity = new Vector2(0f, _rbAlive.velocity.y);
    // }

    // public void OnHit(AttackDetails attackDetails) { }
    #endregion
}
