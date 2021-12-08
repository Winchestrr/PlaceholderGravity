using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image energyBar;

    public static UIController instance;

    private void Start()
    {
        if (instance == null) instance = this;
    }

    public static void RefillBar(float refillRate)
    {
        
    }

    public static void ReduceBar(float reduceRate)
    {

    }
}
