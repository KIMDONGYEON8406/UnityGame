using UnityEngine;

public class EscapeMenu : MonoBehaviour
{
    public GameObject menuUI; // ESC 메뉴 패널

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    private void ToggleMenu()
    {
        bool isActive = !menuUI.activeSelf;
        menuUI.SetActive(isActive);

        // 메뉴 활성화 시 게임 정지, 비활성화 시 게임 재개
        Time.timeScale = isActive ? 0 : 1;
    }

    public void ResumeGame()
    {
        menuUI.SetActive(false);
        Time.timeScale = 1; // 게임 재개
    }

    public void QuitGame()
    {
        Debug.Log("게임 종료");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 실행 중이면 중지
#endif
    }
}

