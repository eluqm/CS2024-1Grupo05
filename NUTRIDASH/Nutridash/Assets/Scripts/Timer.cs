using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Timer : MonoBehaviour
{
public TextMeshProUGUI timerText; // Referencia al TextMeshPro donde se mostrará el temporizador

    private float startTime; // Tiempo en que se inició el temporizador
    private bool isTimerRunning; // Indica si el temporizador está corriendo
     void Start()
    {
        StartTimer();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            float currentTime = Time.time - startTime; // Calcular el tiempo transcurrido desde que se inició el temporizador
            UpdateTimerUI(currentTime); // Actualizar el texto del temporizador en el UI
            // Método para iniciar el temporizador
    public void StartTimer()
        }
    }
}
