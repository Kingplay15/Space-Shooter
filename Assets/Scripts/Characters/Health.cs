using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("General")]
    [SerializeField] int health = 50;
    private int startingHealth = 50;
    public bool isAtMaxHealth { get; private set; } = true;
    public EventHandler OnHealthChangeEvent;

    public int GetHealth() => health;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool applyCameraShake = false;

    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    LevelManager levelManager;

    [Header("Enemy")]
    [SerializeField] private bool isPlayer = false;
    [SerializeField] private int score = 50;
    [SerializeField] private int hasPowerUpChance = 100;
    ScoreKeeper scoreKeeper;
    [SerializeField] GameObject[] powerUps;
    public static EventHandler OnDeathEvent;

    [HideInInspector] public bool isInvulnerable = false;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void Start()
    {
        startingHealth = GetHealth();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            PlayGetHitSound();
            ShakeCamera();
            if (damageDealer.GetIsProjectile())
                damageDealer.GetDestroyed();
        }
        if (isPlayer)
        {
            PowerUp powerUp = collision.GetComponent<PowerUp>();
            if (powerUp != null)
                PlayPowerUpSound();
        }
    }

    void TakeDamage(int damage)
    {
        if (isInvulnerable == false)
            ChangeHealth(-damage);
        if (health <= 0)
        {
            Die();
        }
    }

    public void ChangeHealth(int val)
    {
        health += val;
        isAtMaxHealth = health == startingHealth;
        OnHealthChangeEvent?.Invoke(this, EventArgs.Empty);
    }

    void Die()
    {
        if (isPlayer == false) //If this is an enemy
        {
            SpawnPowerUp();
            scoreKeeper.ModifyScore(score);
            OnDeathEvent?.Invoke(this, EventArgs.Empty);
        }

        else levelManager.LoadGameOver();
        Destroy(gameObject);
    }

    private void SpawnPowerUp()
    {
        int size = powerUps.Length;
        if (size == 0)
            return;

        int chanceSeed = UnityEngine.Random.Range(1, 101); //Every enemy has a certain chance to spawn a Powerup
        if (chanceSeed > hasPowerUpChance)
            return;

        int seed = UnityEngine.Random.Range(0, size); //For generating a random powerup in the list

        GameObject powerUp = Instantiate(powerUps[seed], gameObject.transform.position, Quaternion.identity);
        Rigidbody2D rb = powerUp.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = transform.up * powerUp.GetComponent<PowerUp>().GetDroppingSpeed();
    }

    void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, hitEffect.main.duration + hitEffect.main.startLifetime.constantMax);
        }
    }

    void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake == true)
            cameraShake.Play();
    }

    void PlayGetHitSound()
    {
        audioPlayer.PlayGetHitClip();
    }

    void PlayPowerUpSound()
    {
        audioPlayer.PlayPowerUpClip();
    }
}
