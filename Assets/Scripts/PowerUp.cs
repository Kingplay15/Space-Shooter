using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [SerializeField] float droppingSpeed = 5f;
    protected Health playerHealth;
    public float GetDroppingSpeed()
    {
        return droppingSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth = collision.gameObject.GetComponent<Health>();
            GrantPower(playerHealth);
            Destroy(gameObject);
        }
    }

    protected abstract void GrantPower(Health health);

}
