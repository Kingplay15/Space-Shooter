using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMachinegun : PowerUp
{
    private Shooter shooterPlayer;

    protected override bool CheckPowerExist()
    {
        return player.equippedWeapon == Shooter.Weapon.Machinegun;
    }

    protected override void GrantPower(Player player)
    {
        player.equippedWeapon = Shooter.Weapon.Machinegun;

        shooterPlayer = player.gameObject.GetComponent<Shooter>();
        shooterPlayer.ResetWeapon();
    }
}
