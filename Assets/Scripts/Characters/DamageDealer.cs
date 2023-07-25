using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    [SerializeField] private float projectileSpeed = 10f;
    public float GetProjecttileSpeed => projectileSpeed;

    [SerializeField] private float projectileLifetime = 5f;
    public float GetProjectileLifetime => projectileLifetime;

    [SerializeField] private float baseFireRate = 0.2f;
    public float GetBaseFireRate => baseFireRate;

    [SerializeField] private float fireRateVariance = 0f;
    public float GetFireRateVariance => fireRateVariance;

    [SerializeField] private float minFireRate = 0.1f;
    public float GetMinFireRate => minFireRate;

    [SerializeField] private bool isProjectile = false;
    public bool GetIsProjectile() => isProjectile;

    public int GetDamage() => damage;

    public void GetDestroyed()
    {
        Destroy(gameObject);
    }
}
