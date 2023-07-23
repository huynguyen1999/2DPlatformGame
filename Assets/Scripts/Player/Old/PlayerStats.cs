using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float MaxHealth;
    public GameObject DeathChunkParticle,
        DeathBloodParticle;
    private float _currentHealth;

    private void Start()
    {
        _currentHealth = MaxHealth;
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(DeathChunkParticle, transform.position, DeathChunkParticle.transform.rotation);
        Instantiate(DeathBloodParticle, transform.position, DeathBloodParticle.transform.rotation);
        Destroy(gameObject);
        GameManager.Instance.Respawn();
    }
}
