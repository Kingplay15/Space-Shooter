using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public enum Weapon
    {
        Normal,
        Shotun
    }

    [Header("General")]
    [SerializeField] GameObject[] projectilePrefabs;
    float projectileSpeed = 10f;
    float projectileLifetime = 5f;

    [Header("AI")]
    float baseFireRate = 0.2f;
    float fireRateVariance = 0f;
    float minFireRate = 0.1f;
    [SerializeField] bool useAI = false;

    Coroutine fireCoroutine;
    [HideInInspector] public bool isFiring = false;

    AudioPlayer audioPlayer;
    Player player;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        player = GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (useAI == true)
            isFiring = true;
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring == true && fireCoroutine == null)
            CheckWeapon();
        //fireCoroutine = StartCoroutine(FireContinuously());
        else if (isFiring == false && fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }            
    }

    private void CheckWeapon()
    {
        DamageDealer projectile;
        if (player == null) //If this is a enemy
        {
            projectile = projectilePrefabs[0].GetComponent<DamageDealer>();
            projectileSpeed = projectile.GetProjecttileSpeed;
            projectileLifetime = projectile.GetProjectileLifetime;
            baseFireRate = projectile.GetBaseFireRate;
            fireRateVariance = projectile.GetFireRateVariance;
            minFireRate = projectile.GetMinFireRate;
            fireCoroutine = StartCoroutine(ShootNormal());
            return;
        }

        switch (player.equippedWeapon)
        {
            case Weapon.Normal:                
                projectile = projectilePrefabs[0].GetComponent<DamageDealer>();
                projectileSpeed = projectile.GetProjecttileSpeed;
                projectileLifetime = projectile.GetProjectileLifetime;
                baseFireRate = projectile.GetBaseFireRate;
                fireRateVariance = projectile.GetFireRateVariance;
                minFireRate = projectile.GetMinFireRate;
                fireCoroutine = StartCoroutine(ShootNormal());
                break;
            case Weapon.Shotun:
                StartCoroutine(ShootShotgun());
                break;
        }
    }

    private float ShootGeneralSetup(GameObject projectile)
    {
        float timeToNextFire = UnityEngine.Random.Range
                (baseFireRate - fireRateVariance, baseFireRate + fireRateVariance);
        timeToNextFire = Mathf.Clamp(timeToNextFire, minFireRate, float.MaxValue);
        audioPlayer.PlayShootingClip();
        Destroy(projectile, projectileLifetime);

        return timeToNextFire;
    }

    IEnumerator ShootNormal()
    {
        while(true)
        {
            GameObject projectile = Instantiate(projectilePrefabs[0], transform.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)            
                rb.velocity = transform.up * projectileSpeed;

            float timeToNextFire = ShootGeneralSetup(projectile);

            yield return new WaitForSeconds(timeToNextFire);
        }
    }

    IEnumerator ShootShotgun()
    {
        yield return new WaitForSeconds(2f);
    }
}
