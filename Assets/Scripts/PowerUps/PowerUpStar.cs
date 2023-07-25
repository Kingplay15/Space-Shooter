using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpStar : PowerUp
{
    private Shield shield;

    protected override bool CheckPowerExist()
    {
        return player.haveShield;
    }

    protected override void GrantPower(Player player)
    {
        shield = player.GetShield();
        player.haveShield = true;
        shield.gameObject.SetActive(true);
    }
}
