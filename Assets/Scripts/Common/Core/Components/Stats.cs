using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class Stats : CoreComponent
{
    public event Action OnZeroHealth;
    [SerializeField]
    private float maxHealth;

    private float currentHealth;

    protected override void Awake()
    {
        base.Awake();
        currentHealth = maxHealth;
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnZeroHealth?.Invoke();
        }
    }
    public void IncreaseHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
    }

}