using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Asegúrate de tener esta referencia si usas TextMeshPro

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject statsPanel; // Panel para mostrar las estadísticas del juego
    public Player player; // Referencia al jugador
    public TextMeshProUGUI statsText; // Referencia al TextMeshPro para mostrar las estadísticas

    private float startTime;
    private bool isGameOver = false;

    void Start()
    {
        gameOverPanel.SetActive(false); // Asegurarse de que el panel de Game Over esté oculto al inicio
        statsPanel.SetActive(false); // Ocultar el panel de estadísticas al inicio
        startTime = Time.time; // Registrar el tiempo de inicio del juego
    }

    public void ShowGameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f; // Pausar el juego
        }
    }

    public void Retry()
    {
        Time.timeScale = 1f; // Reanudar el tiempo del juego
        player.ResetPlayer(); // Restablecer el estado del jugador
        gameOverPanel.SetActive(false); // Ocultar el panel de Game Over
        statsPanel.SetActive(false); // Ocultar el panel de estadísticas
        isGameOver = false; // Reiniciar el estado del juego
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void Exit()
    {
        Application.Quit(); // Salir del juego
        SceneManager.LoadScene(0);
    }

    public void CompleteLevel()
    {
        float elapsedTime = Time.time - startTime; // Calcular el tiempo transcurrido
        ShowStats(elapsedTime);
    }

    private void ShowStats(float elapsedTime)
    {
        statsPanel.SetActive(true);
        statsText.text = "¡Ganaste!\n" + $"\n Tiempo: {elapsedTime:F2} segundos";
        Time.timeScale = 0f; // Pausar el juego
    }
}
