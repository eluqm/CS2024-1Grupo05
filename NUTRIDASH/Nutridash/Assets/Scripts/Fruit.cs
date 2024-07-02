using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public enum FruitType { SpeedDecrease, Points, JumpBoost, SpeedBoost }
    public enum FruitTypeHealth { HealthDecrease, HealthIncrease }

    public enum Water { WaterIncrase }

    public FruitType type;
    public FruitTypeHealth typeHealth;
    public Water typeWater;
    public float amount;
    public float amountHealth;
    public float amountWater;
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
                }

                switch (typeHealth)
                {
                    case FruitTypeHealth.HealthDecrease:
                        player.TakeDamage(amountHealth); // Disminuir la salud del jugador
                        break;
                    case FruitTypeHealth.HealthIncrease:
                        player.Heal(amountHealth); // Aumentar la salud del jugador
                        break;
                }

                switch (typeWater)
                {
                    case Water.WaterIncrase:
                        player.IncreaseThirst(amountWater);
                        break;
                }
            }

            // Destruir el GameObject actual (la fruta)
            Destroy(gameObject);
        }
    }

}
