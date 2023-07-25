using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] private AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] private float shootingVolume = 1f;

    [Header("GetHit")]
    [SerializeField] private AudioClip getHitClip;
    [SerializeField] [Range(0f, 1f)] private float getHitVolume = 1f;

    [Header("PowerUp")]
    [SerializeField] private AudioClip powerUpClip;
    [SerializeField] [Range(0f, 1f)] private float powerUpVolume = 1f;

    private void Awake()
    {
        ManageSingleton();
    }

    private void ManageSingleton()
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

    private void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
    }
}
