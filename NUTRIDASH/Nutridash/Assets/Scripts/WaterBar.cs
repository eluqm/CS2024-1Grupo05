using UnityEngine;
using UnityEngine.UI;

public class WaterBar : MonoBehaviour
{
    public Image waterBarFill;

    public void SetMaxWater(float water)
    {
        waterBarFill.fillAmount = 1;
    }

    public void SetWater(float water)
    {
        waterBarFill.fillAmount = water / 100f;
    }
}
