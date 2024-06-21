using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public HealthBar healthBar;
    public float maxHealth = 100f;
    private float currentHealth;
    public GameOverManager gameOverManager; // A�adir referencia al GameOverManager

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (transform.position.y < -10)
        {
            TakeDamage(currentHealth);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetHealth(currentHealth);
    }

    void Die()
    {
        // Mostrar el panel de Game Over
        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOver();
        }
        Debug.Log("Player Died");
    }
}