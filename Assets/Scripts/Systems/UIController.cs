using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Energy")]
    public Image energyBar;
    public TextMeshProUGUI energyText;
    public bool isFilling;

    public static UIController instance;

    private void Start()
    {
        if (instance == null) instance = this;
    }

    private void Update()
    {
        energyText.text = Mathf.Ceil(GravityGun.currentEnergy).ToString();
    }

    public static void UpdateUI()
    {
        instance.energyText.text = GravityGun.currentEnergy.ToString();
        instance.energyBar.fillAmount = GravityGun.currentEnergy / GravityGun.maxEnergy;
    }
}
