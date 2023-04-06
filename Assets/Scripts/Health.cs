using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 20;
    public int GetHealth() => health;

    [SerializeField] ParticleSystem hitEffect;

    [SerializeField] bool applyCameraShake = false;
    CameraShake cameraShake;

    AudioPlayer audioPlayer;

    [SerializeField] bool isPlayer = false;
    [SerializeField] int score = 50;
    ScoreKeeper scoreKeeper;

    LevelManager levelManager;

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
        if(damageDealer!=null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            PlayGetHitSound();
            ShakeCamera();
            damageDealer.GetHit();
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }            
    }

    void Die()
    {
        if (isPlayer == false)
            scoreKeeper.ModifyScore(score);
        else levelManager.LoadGameOver();
        Destroy(gameObject);
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
}
