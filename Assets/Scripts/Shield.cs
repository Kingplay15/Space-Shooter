using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private Player player;
    private Health playerHealth;
    [SerializeField] private float shieldDuration = 5f;

    private void OnEnable()
    {
        player = GetComponentInParent<Player>();
        playerHealth = GetComponentInParent<Health>();
        StartCoroutine(EnableShield());
    }

    private IEnumerator EnableShield()
    {
        playerHealth.isInvulnerable = true;
        yield return new WaitForSeconds(shieldDuration);
        player.haveShield = false;
        playerHealth.isInvulnerable = false;
        gameObject.SetActive(false);
    }
}
