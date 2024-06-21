using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Player player; // Referencia al jugador
    public Timer timer; // Referencia al temporizador

    void Start()
    {
        gameOverPanel.SetActive(false); // Asegurarse de que el panel de Game Over esté oculto al inicio
        timer.StartTimer(); // Iniciar el temporizador al iniciar el GameOverManager
    }

    public void ShowGameOver()
    {
        timer.StopTimer(); // Detener el temporizador al mostrar el Game Over
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // Pausar el juego
    }

    public void Retry()
    {
        Time.timeScale = 1f; // Reanudar el tiempo del juego
        player.ResetPlayer(); // Restablecer el estado del jugador
        timer.StartTimer(); // Reiniciar el temporizador al reiniciar el juego
        gameOverPanel.SetActive(false); // Ocultar el panel de Game Over
    }

    public void Exit()
    {
        Application.Quit(); // Salir del juego
            SceneManager.LoadScene(0);
        }
}
