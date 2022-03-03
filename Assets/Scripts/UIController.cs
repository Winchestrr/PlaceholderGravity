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

    public static void EnergyBarUse()
    {
        instance.energyBar.fillAmount = GravityGun.currentEnergy / GravityGun.maxEnergy;
        instance.energyText.text = GravityGun.currentEnergy.ToString();
    }

    public IEnumerator EnergyBarFillIE()
    {
        isFilling = true;

        for(float i = 0; i < 100; i++)
        {
            energyBar.fillAmount += 0.01f;
            
            if(energyBar.fillAmount == 1)
            {
                isFilling = false;
                StopCoroutine(EnergyBarFillIE());
            }
            else
            {
                yield return new WaitForSeconds(GravityGun.energyFillRate / 100);
            }
        }
    }
}
