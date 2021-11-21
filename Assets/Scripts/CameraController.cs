using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject mainCamera;

    void CameraSet180()
    {
        mainCamera.transform.Rotate(5.07f, 0, 180);
    }

    void CameraSet0()
    {
        mainCamera.transform.Rotate(5.07f, 0, 0);
    }
}
