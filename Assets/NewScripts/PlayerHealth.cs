using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        HealthBar.UpdateHealth(currentHealth, maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        HealthBar.UpdateHealth(currentHealth, maxHealth);
        Debug.Log($"🔥 PlayerHealth: 체력 {damage} 감소! 현재 체력: {currentHealth}");

        if (currentHealth <= 0)
        {
            Debug.Log("💀 플레이어 사망!");
            // 🚀 사망 처리 (리스폰 또는 게임 오버)
        }
    }
}






