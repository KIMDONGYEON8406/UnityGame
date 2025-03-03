using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscapeMenu : MonoBehaviour
{
    public GameObject escapeMenuPanel; // ESC 키로 열릴 UI 패널
    public Button returnButton; // 돌아가기 버튼
    public Button exitButton; // 게임 종료 버튼
    private bool isMenuActive = false; // 메뉴 활성화 상태

    private void Awake()
    {
        if (FindObjectsOfType<EscapeMenu>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (escapeMenuPanel != null)
        {
            escapeMenuPanel.SetActive(false);
            Transform canvasTransform = escapeMenuPanel.transform.parent;

            if (canvasTransform != null)
            {
                DontDestroyOnLoad(canvasTransform.gameObject);
                Debug.Log("✅ Canvas도 DontDestroyOnLoad 적용 완료!");
            }
            else
            {
                Debug.LogError("❌ Canvas가 존재하지 않음! Inspector에서 확인하세요.");
            }
        }
        else
        {
            Debug.LogError("❌ ESC 메뉴: escapeMenuPanel이 설정되지 않음! Inspector에서 확인하세요.");
        }

        if (returnButton != null)
        {
            returnButton.onClick.AddListener(CloseMenu);
            Debug.Log("✅ ESC 메뉴: 돌아가기 버튼 정상 연결");
        }
        else
        {
            Debug.LogError("❌ ESC 메뉴: returnButton이 설정되지 않음!");
        }

        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitGame);
            Debug.Log("✅ ESC 메뉴: 종료 버튼 정상 연결");
        }
        else
        {
            Debug.LogError("❌ ESC 메뉴: exitButton이 설정되지 않음!");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    private void ToggleMenu()
    {
        if (escapeMenuPanel == null) return;

        isMenuActive = !isMenuActive;
        escapeMenuPanel.SetActive(isMenuActive);
        Time.timeScale = isMenuActive ? 0 : 1;
    }

    private void CloseMenu()
    {
        if (escapeMenuPanel == null) return;
        isMenuActive = false;
        escapeMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Debug.Log("❌ ESC 메뉴에서 게임 종료!");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}








