using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Importar el namespace para TextMeshPro

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
        }
    }

    // Método para iniciar el temporizador
    public void StartTimer()
    {
        startTime = Time.time; // Guardar el tiempo actual como el tiempo de inicio del temporizador
        isTimerRunning = true; // Marcar el temporizador como corriendo
    }

    // Método para detener el temporizador
    public void StopTimer()
    {
        isTimerRunning = false; // Marcar el temporizador como detenido
    }

    // Método para actualizar el texto del temporizador en el UI TextMeshPro
    private void UpdateTimerUI(float currentTime)
    {
        if (timerText != null)
        {
            timerText.text = FormatTime(currentTime); // Actualizar el texto con el tiempo formateado
        }
    }

    // Método para formatear el tiempo en minutos y segundos
    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f); // Obtener los minutos enteros
        int seconds = Mathf.FloorToInt(time % 60f); // Obtener los segundos enteros

        return string.Format("{0:00}:{1:00}", minutes, seconds); // Formato MM:SS
    }
}
