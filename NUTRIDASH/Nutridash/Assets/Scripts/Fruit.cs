using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public enum FruitType { SpeedDecrease, Points, JumpBoost, SpeedBoost, HealthDecrease }

    public FruitType type;
    public float amount; // Cantidad que afecta los valores como velocidad, puntos o salud
    public float jumpDecreaseAmount; // Cantidad para disminuir el salto (para SpeedDecrease)
    public float duration = 5f; // Duración del efecto en segundos

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                switch (type)
                {
                    case FruitType.SpeedDecrease:
                        player.ApplySpeedDecrease(amount, jumpDecreaseAmount, duration);
                        break;
                    case FruitType.Points:
                        player.AddPoints((int)amount); // Convertir a int si es necesario
                        break;
                    case FruitType.JumpBoost:
                        player.ApplyJumpBoost(amount, duration);
                        break;
                    case FruitType.SpeedBoost:
                        player.ApplySpeedBoost(amount, duration);
                        break;
                    case FruitType.HealthDecrease:
                        player.TakeDamage(amount); // Disminuir la salud del jugador
                        break;
                }
            }
            Destroy(gameObject);
        }
    }
}