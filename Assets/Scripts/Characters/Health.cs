using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("General")]
    [SerializeField] int health = 20;
    public int GetHealth() => health;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] bool applyCameraShake = false;

    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    LevelManager levelManager;

    [Header("Enemy")]
    [SerializeField] bool isPlayer = false;
    [SerializeField] int score = 50;
    ScoreKeeper scoreKeeper;
    [SerializeField] GameObject[] powerUps;

    [HideInInspector] public bool isInvulnerable = false;

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
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
            damageDealer.GetHit();
        }
        if (isPlayer == true)
        {
            PowerUp powerUp = collision.GetComponent<PowerUp>();
            if (powerUp != null)
                PlayPowerUpSound();
        }
    }

    void TakeDamage(int damage)
    {
        if (isInvulnerable == false) 
            health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isPlayer == false) //If this is an enemy
        {
            SpawnPowerUp();
            scoreKeeper.ModifyScore(score);
        }

        else levelManager.LoadGameOver();
        Destroy(gameObject);

    }

    private void SpawnPowerUp()
    {
        int size = powerUps.Length;
        int seed = Random.Range(0, size - 1); //For generating a random powerup in the list

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
