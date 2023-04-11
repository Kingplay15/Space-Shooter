using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpStar : PowerUp
{
    protected override void GrantPower(Health health)
    {
        health.isInvulnerable = true;
    }
}
