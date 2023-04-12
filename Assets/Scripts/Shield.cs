using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    Player player;
    Health playerHealth;
    [SerializeField] float shieldDuration = 5f;

    private void OnEnable()
    {
        player = GetComponentInParent<Player>();
        playerHealth = GetComponentInParent<Health>();
        StartCoroutine(EnableShield());
    }

    IEnumerator EnableShield()
    {
        playerHealth.isInvulnerable = true;
        yield return new WaitForSeconds(shieldDuration);
        player.haveShield = false;
        playerHealth.isInvulnerable = false;
        gameObject.SetActive(false);
    }
}
