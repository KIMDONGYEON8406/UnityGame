using UnityEngine;

public class EscapeMenu : MonoBehaviour
{
    public GameObject menuUI; // ESC �޴� �г�

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

        // �޴� Ȱ��ȭ �� ���� ����, ��Ȱ��ȭ �� ���� �簳
        Time.timeScale = isActive ? 0 : 1;
    }

    public void ResumeGame()
    {
        menuUI.SetActive(false);
        Time.timeScale = 1; // ���� �簳
    }

    public void QuitGame()
    {
        Debug.Log("���� ����");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // �����Ϳ��� ���� ���̸� ����
#endif
    }
}

