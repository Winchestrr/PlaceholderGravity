using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityObject : MonoBehaviour
{
    private Rigidbody _rb;
    private ConstantForce _cf;

    public float invertedGravity;
    public float floatGravity;

    private float tempGravity;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _cf = GetComponent<ConstantForce>();

        invertedGravity = GameSystem.newGravity.y * (-2) * _rb.mass;
        floatGravity = GameSystem.newGravity.y * (-1) * _rb.mass;
    }

    public void DrainGravity(float amount)
    {
        if (_cf.force.y != floatGravity)
        {
            tempGravity = _cf.force.y;
            _cf.force.Set(0, tempGravity + amount, 0);
        }
        else Debug.Log("Object has no gravity.");
    }
}
