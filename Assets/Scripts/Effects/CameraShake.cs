using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 1f;
    [SerializeField] private float shakeMagnitude = 0.5f;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }
    
    public void Play()
    {
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration) 
        {
            transform.position = initialPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = initialPosition;
    }
}
