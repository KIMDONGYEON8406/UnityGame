using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Text enemiesLeftText;
    public Slider playerHPBar; // 플레이어 HP 바 추가
    public GameObject enemiesLeftUI; // Enemies Left UI 오브젝트
    public Text totalKillsText; //  누적 처치 수 UI 추가

    private int totalKills = 0; //  누적 처치 수 변수

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

    //  `SetUIVisibility()` 복구 → Enemies Left UI 표시 여부 제어
    public void SetUIVisibility(bool isVisible)
    {
        if (enemiesLeftUI != null)
        {
            enemiesLeftUI.SetActive(isVisible);
        }

        if (totalKillsText != null) //  Total Kills UI도 숨기거나 표시
        {
            totalKillsText.gameObject.SetActive(isVisible);
        }
    }

    //  누적 처치 수 업데이트 함수 추가
    public void UpdateTotalKills()
    {
        totalKills++;
        if (totalKillsText != null)
        {
            totalKillsText.text = "Total Kills: " + totalKills;
        }
    }
}



