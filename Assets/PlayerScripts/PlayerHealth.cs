using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHP = 100f;
    private float currentHP;

    private void Start()
    {
        currentHP = maxHP;
        UIManager.instance.UpdatePlayerHP(currentHP, maxHP); // 초기 HP 설정
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        UIManager.instance.UpdatePlayerHP(currentHP, maxHP);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("플레이어 사망! (게임 오버 처리 가능)");
        GameManager.instance.GameOver(); // 게임 오버 UI 실행
    }
}

