using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    protected Rigidbody rb;

    protected float damage;
    protected float speed;
    public float lifetime;

    private void Update()
    {
        MoveBullet();
    }

    private void MoveBullet()
    {
        rb.velocity = transform.forward * speed;
    }

    public virtual void Launch(float _damage, float _speed)
    {
        Vector3 bulletDirection = transform.forward;

        rb = GetComponent<Rigidbody>();
        rb.velocity = bulletDirection * _speed;

        speed = _speed;
        damage = _damage;

        Destroy(gameObject, lifetime);
    }
}
