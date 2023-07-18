using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public enum Weapon
    {
        Normal,
        Shotgun,
        Machinegun,
        Aiminggun,
        MiniShotgun
    }

    [Header("General")]
    [SerializeField] GameObject[] projectilePrefabs;
    float projectileSpeed = 10f;
    float projectileLifetime = 5f;

    [Header("AI")]
    [SerializeField] bool useAI = false;
    [SerializeField] Weapon aIWeapon;


    [Header("Player")]
    [SerializeField] float machineGunPaddingLeft = -0.5f;
    [SerializeField] float machineGunPaddingRight = 0.5f;
    float baseFireRate = 0.2f;
    float fireRateVariance = 0f;
    float minFireRate = 0.1f;
    Coroutine fireCoroutine;
    [HideInInspector] public bool isFiring = false;
    float timeToNextFire = 0f;

    AudioPlayer audioPlayer;
    Player player;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        if (useAI)
            player = FindObjectOfType<Player>();
        else player = GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //The enemies are not allowed to shoot offscreen
        if (useAI)
        {
            if (IsOnScreen())
            {
                timeToNextFire = 0f;
                isFiring = true;
            }
            else
            {
                isFiring = false;
                if (fireCoroutine != null)
                {
                    StopCoroutine(fireCoroutine);
                    fireCoroutine = null; 
                }
            }
        }

        Fire();
    }

    //The object is visible on the camera
    private bool IsOnScreen()
    {
        return !(gameObject.transform.position.x < GeneralData.minBound.x || gameObject.transform.position.x > GeneralData.maxBound.x ||
            gameObject.transform.position.y < GeneralData.minBound.y || gameObject.transform.position.y > GeneralData.maxBound.y);
    }

    void Fire()
    {
        if (isFiring == true && fireCoroutine == null && timeToNextFire == 0f)
        {
            CheckWeapon();
        }

        else if (isFiring == false && fireCoroutine != null)
        {
            ResetWeapon();
        }
    }

    private IEnumerator WaitForNextFire()
    {
        yield return new WaitForSeconds(timeToNextFire);
        timeToNextFire = 0f;
    }

    public void ResetWeapon()
    {
        if (fireCoroutine != null)
            StopCoroutine(fireCoroutine);
        fireCoroutine = null;
        StartCoroutine(WaitForNextFire());
    }

    private void FireStatsSetUp(DamageDealer projectile)
    {
        projectileSpeed = projectile.GetProjecttileSpeed;
        projectileLifetime = projectile.GetProjectileLifetime;
        baseFireRate = projectile.GetBaseFireRate;
        fireRateVariance = projectile.GetFireRateVariance;
        minFireRate = projectile.GetMinFireRate;
    }

    private void CheckWeapon()
    {
        DamageDealer projectile;
        if (useAI) //If this is an enemy
        {
            projectile = projectilePrefabs[0].GetComponent<DamageDealer>();
            FireStatsSetUp(projectile);
            switch (aIWeapon)
            {
                case Weapon.Normal:
                    fireCoroutine = StartCoroutine(ShootNormal());
                    return;
                case Weapon.Aiminggun:
                    fireCoroutine = StartCoroutine(ShootAiminggun());
                    return;
                case Weapon.MiniShotgun:
                    fireCoroutine = StartCoroutine(ShootMiniShotgun());
                    return;
            }
            return;
        }

        switch (player.equippedWeapon)
        {
            case Weapon.Normal:
                projectile = projectilePrefabs[0].GetComponent<DamageDealer>();
                FireStatsSetUp(projectile);
                fireCoroutine = StartCoroutine(ShootNormal());
                break;
            case Weapon.Shotgun:
                projectile = projectilePrefabs[1].GetComponent<DamageDealer>();
                FireStatsSetUp(projectile);
                fireCoroutine = StartCoroutine(ShootShotgun());
                break;
            case Weapon.Machinegun:
                projectile = projectilePrefabs[2].GetComponent<DamageDealer>();
                FireStatsSetUp(projectile);
                fireCoroutine = StartCoroutine(ShootMachineGun());
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
                        case 0: //30 degree to the left upward, magnitude = 1
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

    IEnumerator ShootMachineGun()
    {
        while (true)
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject projectile;
                if (i == 0)
                    projectile = Instantiate(projectilePrefabs[2],
                        transform.position + new Vector3(machineGunPaddingLeft, 0), Quaternion.identity);
                else projectile = Instantiate(projectilePrefabs[2],
                    transform.position + new Vector3(machineGunPaddingRight, 0), Quaternion.identity);

                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                if (rb != null)
                    rb.velocity = transform.up * projectileSpeed;

                Destroy(projectile, projectileLifetime);
            }

            timeToNextFire = ShootGeneralSetup();
            yield return new WaitForSeconds(timeToNextFire);
            timeToNextFire = 0f;
        }
    }

    private IEnumerator ShootAiminggun() //Shoot a projectile at the target's position
    {
        while (true)
        {
            GameObject projectile = Instantiate(projectilePrefabs[0], transform.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            Vector3 targetPos = new Vector3();
            if (useAI)
                targetPos = player.gameObject.transform.position;
            if (rb != null)
                rb.velocity = (targetPos - transform.position).normalized * projectileSpeed;

            timeToNextFire = ShootGeneralSetup();
            Destroy(projectile, projectileLifetime);
            yield return new WaitForSeconds(timeToNextFire);
        }
    }

    private IEnumerator ShootMiniShotgun()
    {
        while (true)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject projectile = Instantiate(projectilePrefabs[0], transform.position, Quaternion.identity);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    switch (i)
                    {
                        case 0: //30 degree to the left downward, magnitude = 1
                            rb.velocity = new Vector3(-0.5f, -0.87f) * projectileSpeed; //0.5f = cos(60 degree), 0.87f = cos(30 degree) approximately
                            break;
                        case 1: //straight down
                            rb.velocity = new Vector3(0, -1) * projectileSpeed;
                            break;
                        case 2: //30 degree to the right
                            rb.velocity = new Vector3(0.5f, -0.87f) * projectileSpeed;
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
