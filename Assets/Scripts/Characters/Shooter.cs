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
    float timeToNextFire = 0f;

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
        if (isFiring == true && fireCoroutine == null && timeToNextFire == 0f) 
        {
            CheckWeapon();
        }
            
        //fireCoroutine = StartCoroutine(FireContinuously());
        else if (isFiring == false && fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
            StartCoroutine(WaitForNextFire());          
        }
    }

    IEnumerator WaitForNextFire()
    {
        yield return new WaitForSeconds(timeToNextFire);
        timeToNextFire = 0f;
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
                projectile = projectilePrefabs[1].GetComponent<DamageDealer>();
                projectileSpeed = projectile.GetProjecttileSpeed;
                projectileLifetime = projectile.GetProjectileLifetime;
                baseFireRate = projectile.GetBaseFireRate;
                fireRateVariance = projectile.GetFireRateVariance;
                minFireRate = projectile.GetMinFireRate;
                fireCoroutine = StartCoroutine(ShootShotgun());
                break;
        }
    }

    private float ShootGeneralSetup()
    {
        float nextFire = UnityEngine.Random.Range
                (baseFireRate - fireRateVariance, baseFireRate + fireRateVariance);
        nextFire = Mathf.Clamp(nextFire, minFireRate, float.MaxValue);
        audioPlayer.PlayShootingClip();
        return nextFire;
    }

    IEnumerator ShootNormal()
    {
        while (true)
        {
            GameObject projectile = Instantiate(projectilePrefabs[0], transform.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.velocity = transform.up * projectileSpeed;

            timeToNextFire = ShootGeneralSetup();           
            Destroy(projectile, projectileLifetime);
            yield return new WaitForSeconds(timeToNextFire);
        }       
    }

    IEnumerator ShootShotgun()
    {
        while (true)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject projectile = Instantiate(projectilePrefabs[1], transform.position, Quaternion.identity);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                if (rb != null)
                {                    
                    switch (i) 
                    {
                        case 0: //30 degree to the left, magnitude = 1
                            rb.velocity = new Vector3(-0.5f, 0.87f) * projectileSpeed; //0.5f = cos(60 degree), 0.87f = cos(30 degree) approximately
                            break;
                        case 1: //15 degree to the left
                            rb.velocity = new Vector3(-0.26f, 0.97f) * projectileSpeed;
                            break;
                        case 2: //straight up
                            rb.velocity = new Vector3(0, 1) * projectileSpeed;
                            break;
                        case 3: //15 degree to the right
                            rb.velocity = new Vector3(0.26f, 0.97f) * projectileSpeed;
                            break;
                        case 4: //30 degree to the right
                            rb.velocity = new Vector3(0.5f, 0.87f) * projectileSpeed;
                            break;
                    }
                }
                Destroy(projectile, projectileLifetime);
            }

            timeToNextFire = ShootGeneralSetup();
            yield return new WaitForSeconds(timeToNextFire);
            timeToNextFire = 0f;
        }
    }
}
