using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Timer : MonoBehaviour
{
public TextMeshProUGUI timerText; // Referencia al TextMeshPro donde se mostrará el temporizador

    private float startTime; // Tiempo en que se inició el temporizador
    private bool isTimerRunning; // Indica si el temporizador está corriendo
}
