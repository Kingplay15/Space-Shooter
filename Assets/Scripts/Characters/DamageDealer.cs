using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 10;
    [SerializeField] float projectileSpeed = 10f;
    public float GetProjecttileSpeed => projectileSpeed;
    [SerializeField] float projectileLifetime = 5f;
    public float GetProjectileLifetime => projectileLifetime;
    [SerializeField] float baseFireRate = 0.2f;
    public float GetBaseFireRate => baseFireRate;
    [SerializeField] float fireRateVariance = 0f;
    public float GetFireRateVariance => fireRateVariance;
    [SerializeField] float minFireRate = 0.1f;
    public float GetMinFireRate => minFireRate;

    public int GetDamage() => damage;

    public void GetHit()
    {
        Destroy(gameObject);
    }
}
