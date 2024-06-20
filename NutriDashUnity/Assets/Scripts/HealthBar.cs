using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarFill;

    public void SetMaxHealth(float health)
    {
        healthBarFill.fillAmount = 1;
    }

    public void SetHealth(float health)
    {
        healthBarFill.fillAmount = health / 100f;
    }
}
