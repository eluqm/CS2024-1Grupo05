using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThirst : MonoBehaviour
{
    public WaterBar waterBar;
    public float maxThirst = 100f;
    private float currentThirst;


    public LayerMask groundLayer; // Capa que representa el suelo
    public float groundCheckDistance = 0.1f; // Distancia para verificar si el jugador está en el suelo

    // Evento para notificar sed baja
    public delegate void ThirstLowEvent();
    public event ThirstLowEvent OnThirstLow;

    void Start()
    {
        currentThirst = maxThirst;
        waterBar.SetMaxWater(maxThirst);
    }

    public void DecreaseThirst(float amount)
    {
        currentThirst -= amount;
        currentThirst = Mathf.Clamp(currentThirst, 0, maxThirst);
        waterBar.SetWater(currentThirst);

        // Verificar si la sed está baja y notificar al jugador
        if (currentThirst <= maxThirst * 0.4f)
        {
            if (OnThirstLow != null)
            {
                OnThirstLow(); // Disparar el evento de sed baja
            }
        }
    }

    public void IncreaseThirst(float amount)
    {
        currentThirst += amount;
        currentThirst = Mathf.Clamp(currentThirst, 0, maxThirst);
        waterBar.SetWater(currentThirst);
    }
}
