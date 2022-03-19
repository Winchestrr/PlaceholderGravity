using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton2 : MonoBehaviour
{
    public float thresholdValue = 0.1f;
    public float deadZone = 0.025f;

    public UnityEvent onPressed;
    public UnityEvent onReleased;

    private bool _isPressed;
    private Vector3 _startPos;
    private ConfigurableJoint _joint;

    private void Start()
    {
        _startPos = transform.localPosition;
        _joint = GetComponent<ConfigurableJoint>();
    }

    private void Update()
    {
        if(!_isPressed && GetValue() + thresholdValue >= 1)
            Pressed();

        if (_isPressed && GetValue() - thresholdValue <= 0)
            Released();
    }

    private float GetValue()
    {
        var value = Vector3.Distance(_startPos, transform.localPosition) / _joint.linearLimit.limit;
        if (Mathf.Abs(value) < deadZone)
            value = 0;

        return Mathf.Clamp(value, -1f, 1f);
    }

    private void Pressed()
    {
        _isPressed = true;
        onPressed.Invoke();
        Debug.Log("Pressed");
    }

    private void Released()
    {
        _isPressed = false;
        onReleased.Invoke();
        Debug.Log("Released");
    }
}
