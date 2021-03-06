using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardWeapon : WeaponBase
{
    protected override void Shoot()
    {
        base.Shoot();

        GameObject shotBullet = Instantiate(bullet, barrelEnd.position, transform.rotation);
        shotBullet.GetComponent<BulletBase>().Launch(damage, bulletSpeed);
    }
}
