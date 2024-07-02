using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public GameOverManager gameOverManager; // Referencia al GameOverManager
    private bool levelCompleted = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !levelCompleted)
        {
            levelCompleted = true;
            gameOverManager.CompleteLevel(); // Llamar al método de completar nivel en el GameOverManager
        }
    }
}
