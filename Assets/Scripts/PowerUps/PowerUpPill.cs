using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPill : PowerUp
{
    [SerializeField] private int healthGrant = 10;

    protected override bool CheckPowerExist()
    {
        Health playerHealth = player.GetComponent<Health>();
        return playerHealth.isAtMaxHealth;
    }

    protected override void GrantPower(Player player)
    {
        Health playerHealth = player.GetComponent<Health>();
        playerHealth.ChangeHealth(healthGrant);
    }
}
