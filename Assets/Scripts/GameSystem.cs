using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public Vector3 newGravity;

    void Start()
    {
        Physics.gravity = newGravity;
    }
}