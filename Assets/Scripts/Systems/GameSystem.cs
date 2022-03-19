using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static Vector3 newGravity;
    public Vector3 newGravity_disp;

    void Start()
    {
        newGravity = newGravity_disp;
        Physics.gravity = newGravity;
    }
}
