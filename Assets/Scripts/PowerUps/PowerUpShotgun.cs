using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpShotgun : PowerUp
{
    private Shooter shooterPlayer;
    protected override bool CheckPowerExist()
    {
        return player.equippedWeapon == Shooter.Weapon.Shotgun;
    }

    protected override void GrantPower(Player player)
    {
        player.equippedWeapon = Shooter.Weapon.Shotgun;

        shooterPlayer = player.gameObject.GetComponent<Shooter>();
        shooterPlayer.ResetWeapon();
    }
}
