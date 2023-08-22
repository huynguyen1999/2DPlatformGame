using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public event Action OnInit;

    public event Action<ProjectileDataPackage> OnReceiveDataPackage;

    public Rigidbody2D RB { get; private set; }

    public void Init()
    {
        OnInit?.Invoke();
        StartCoroutine(DestroyCoroutine());
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
    /* This function is called before Init from the weapon. Any weapon component can use this to function to pass along information that the projectile might need that is
    weapon specific, such as: damage amount, draw length modifiers, etc. */
    public void SendDataPackage(ProjectileDataPackage dataPackage)
    {
        OnReceiveDataPackage?.Invoke(dataPackage);
    }

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }
}