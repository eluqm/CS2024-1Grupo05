using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float startingTime = 180f; // Tiempo inicial en segundos
    private float currentTime;
    public TextMeshProUGUI timerText;
    private bool isRunning = true;

    void Start()
    {
        ResetTimer(); // Inicializar el tiempo al comienzo del juego
    }

    void Update()
    {
        if (isRunning)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = 0;
                isRunning = false;
                FindObjectOfType<GameOverManager>().ShowGameOver();
            }
            UpdateTimerText();
        }
    }

    public void ResetTimer()
    {
        currentTime = startingTime;
        isRunning = true;
    }

    private void UpdateTimerText()
    {
        timerText.text = currentTime.ToString("F2");
    }
}
