using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projecttileSpeed = 10f;
    [SerializeField] float projecttileLifetime = 5f;

    [Header("AI")]
    [SerializeField] float baseFireRate = 0.2f;
    [SerializeField] float fireRateVariance = 0f;
    [SerializeField] float minFireRate = 0.1f;
    [SerializeField] bool useAI = false;

    Coroutine fireCoroutine;
    [HideInInspector] public bool isFiring = false;

    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
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
            fireCoroutine = StartCoroutine(FireContinuously());
        else if (isFiring == false && fireCoroutine != null) 
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }            
    }

    IEnumerator FireContinuously()
    {
        while(true)
        {
            GameObject projecttile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = projecttile.GetComponent<Rigidbody2D>();
            if (rb != null)            
                rb.velocity = transform.up * projecttileSpeed;
            float timeToNextFire = UnityEngine.Random.Range
                (baseFireRate - fireRateVariance, baseFireRate + fireRateVariance);
            timeToNextFire = Mathf.Clamp(timeToNextFire, minFireRate, float.MaxValue);

            audioPlayer.PlayShootingClip();

            Destroy(projecttile, projecttileLifetime);
            yield return new WaitForSeconds(timeToNextFire);
        }
    }
}
