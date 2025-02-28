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
        UIManager.instance.UpdatePlayerHP(currentHP, maxHP); // �ʱ� HP ����
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
        Debug.Log("�÷��̾� ���! (���� ���� ó�� ����)");
        GameManager.instance.GameOver(); // ���� ���� UI ����
    }
}

