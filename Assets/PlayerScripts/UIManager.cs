using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Text enemiesLeftText;
    public Slider playerHPBar; // �÷��̾� HP �� �߰�
    public GameObject enemiesLeftUI; // Enemies Left UI ������Ʈ
    public Text totalKillsText; //  ���� óġ �� UI �߰�

    private int totalKills = 0; //  ���� óġ �� ����

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void UpdateEnemyCount(int count)
    {
        if (enemiesLeftText != null)
        {
            enemiesLeftText.text = "Enemies Left: " + count;
        }
    }

    public void UpdatePlayerHP(float currentHP, float maxHP)
    {
        if (playerHPBar != null)
        {
            playerHPBar.value = currentHP / maxHP;
        }
    }

    //  `SetUIVisibility()` ���� �� Enemies Left UI ǥ�� ���� ����
    public void SetUIVisibility(bool isVisible)
    {
        if (enemiesLeftUI != null)
        {
            enemiesLeftUI.SetActive(isVisible);
        }

        if (totalKillsText != null) //  Total Kills UI�� ����ų� ǥ��
        {
            totalKillsText.gameObject.SetActive(isVisible);
        }
    }

    //  ���� óġ �� ������Ʈ �Լ� �߰�
    public void UpdateTotalKills()
    {
        totalKills++;
        if (totalKillsText != null)
        {
            totalKillsText.text = "Total Kills: " + totalKills;
        }
    }
}



