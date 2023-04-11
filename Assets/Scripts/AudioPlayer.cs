using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 1f;

    [Header("GetHit")]
    [SerializeField] AudioClip getHitClip;
    [SerializeField] [Range(0f, 1f)] float getHitVolume = 1f;

    [Header("PowerUp")]
    [SerializeField] AudioClip powerUpClip;
    [SerializeField] [Range(0f, 1f)] float powerUpVolume = 1f;

    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        int instanceCount = FindObjectsOfType(GetType()).Length;
        if (instanceCount > 1)
            Destroy(gameObject);
        else DontDestroyOnLoad(gameObject);
    }

    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayGetHitClip()
    {
        PlayClip(getHitClip, getHitVolume);
    }

    public void PlayPowerUpClip()
    {
        PlayClip(powerUpClip, powerUpVolume);
    }

    void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
    }
}
