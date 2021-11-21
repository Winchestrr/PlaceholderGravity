using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject mainCamera;

    public Transform target;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;

    private void LateUpdate()
    {
        CameraFollow();
    }

    void CameraSet180()
    {
        mainCamera.transform.Rotate(-5.07f, 0, 180);
    }

    void CameraSet0()
    {
        mainCamera.transform.Rotate(5.07f, 0, 0);
    }

    void CameraFollow()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothPosition;
    }
}
