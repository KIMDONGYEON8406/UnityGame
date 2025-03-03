using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public static HealthBar instance; // ✅ 다른 스크립트에서 접근할 수 있도록 public static 선언
    public GameObject healthPanel; // ✅ 전체 체력 바 UI 패널 (항상 활성화 상태)
    public Slider healthSlider; // ✅ 체력 바 슬라이더

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ✅ 씬 변경 시 HealthBar 유지
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        if (healthPanel != null)
        {
            healthPanel.SetActive(true); // ✅ 게임 시작 시 항상 체력 바 유지
            Debug.Log("🟢 HealthBar: HealthPanel 항상 활성화됨.");
        }
        else
        {
            Debug.LogError("❌ HealthBar: healthPanel이 설정되지 않음! Inspector에서 확인 필요!");
        }

        if (healthSlider == null)
        {
            Debug.LogError("❌ HealthBar: healthSlider가 연결되지 않음! Inspector에서 확인 필요!");
        }
    }

    public static void UpdateHealth(float currentHealth, float maxHealth)
    {
        if (instance != null && instance.healthSlider != null)
        {
            instance.healthSlider.value = currentHealth / maxHealth;
            Debug.Log($"🟢 HealthBar 업데이트: {currentHealth} / {maxHealth}");
        }
    }
}







