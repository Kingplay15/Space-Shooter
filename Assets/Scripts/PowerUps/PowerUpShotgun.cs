using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShotgun : PowerUp
{
    protected override bool CheckPowerExist()
    {
        return player.equippedWeapon == Shooter.Weapon.Shotun;
    }

    protected override void GrantPower(Player player)
    {
        player.equippedWeapon = Shooter.Weapon.Shotun;
    }
}
