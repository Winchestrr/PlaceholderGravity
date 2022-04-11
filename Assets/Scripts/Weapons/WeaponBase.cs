using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    public Transform barrelEnd;
    public GameObject bullet;

    public float damage;
    public float bulletSpeed;
    public float timeBetweenShots;
    public float currentTimeBetweenShots;
    public float lastShotTime;

    private void Start()
    {
        currentTimeBetweenShots = timeBetweenShots;
    }

    public virtual void TryShoot()
    {
        if (CanShoot()) Shoot();
    }

    protected virtual void Shoot()
    {
        lastShotTime = Time.time;
    }

    private bool CanShoot()
    {
        if (Time.time > lastShotTime + currentTimeBetweenShots) return true;
        else return false;
    }
}
