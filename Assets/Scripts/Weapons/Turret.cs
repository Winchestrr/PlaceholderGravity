using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : StandardWeapon
{
    public bool canShoot;

    private void Update()
    {
        if (canShoot) TryShoot();
    }
}
