using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Player player; // Referencia al jugador

    void Start()
    {
        gameOverPanel.SetActive(false); // Asegurarse de que el panel de Game Over est� oculto al inicio
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // Pausar el juego
    }

    public void Retry()
    {
        Time.timeScale = 1f; // Reanudar el tiempo del juego
        player.ResetPlayer(); // Restablecer el estado del jugador
        gameOverPanel.SetActive(false); // Ocultar el panel de Game Over
    }

    public void Exit()
    {
        Application.Quit(); // Salir del juego
    }
}
