using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private FadeEffect fadeEffect;

    public GameObject gameOverUI; // ���� ���� UI �߰�

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "TitleScene")
        {
            fadeEffect = FindObjectOfType<FadeEffect>();
        }
    }

    public void FadeInOut(System.Action onFadeComplete)
    {
        if (fadeEffect == null)
        {
            fadeEffect = FindObjectOfType<FadeEffect>();
            if (fadeEffect == null) return;
        }

        fadeEffect.StartFadeInOut(onFadeComplete);
    }

    public void LoadScene(string sceneName)
    {
        Debug.Log($"�� ��ȯ �õ�: {sceneName}");

        // TitleScene �� TrainingRoom �̵� �� ��� �� �ε� (���̵� ȿ�� X)
        if (SceneManager.GetActiveScene().name == "TitleScene" && sceneName == "TrainingRoom")
        {
            Debug.Log("TitleScene �� TrainingRoom ��� ��ȯ");
            SceneManager.LoadScene(sceneName);
            return;
        }

        // �ٸ� �� ��ȯ �� ���̵� ȿ�� ���� (TrainingRoom �� Stage ��)
        if (fadeEffect == null)
        {
            fadeEffect = FindObjectOfType<FadeEffect>();
        }

        fadeEffect?.StartFadeInOut(() =>
        {
            Debug.Log($"���̵� ȿ�� ���� �� �� ��ȯ: {sceneName}");
            SceneManager.LoadScene(sceneName);
        });
    }

    //  ���� ���� ó�� �߰�
    public void GameOver()
    {
        Debug.Log("���� ����!");
        gameOverUI.SetActive(true); // ���� ���� UI Ȱ��ȭ
        Time.timeScale = 0f; // ���� �Ͻ� ����
    }

    //  ���� ����� ��� �߰�
    public void RestartGame()
    {
        Time.timeScale = 1f; // �ð� ����ȭ
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // ���� �� �ٽ� �ε�
    }

    //  ���� �޴��� �̵� ��� �߰�
    public void GoToTitle()
    {
        Time.timeScale = 1f; // �ð� ����ȭ
        SceneManager.LoadScene("TitleScene"); // ���� �޴� ������ �̵� (�� �̸� ���� ����)
    }
}




//public class GameManager : MonoBehaviour
//{
//    public static GameManager instance;
//    private FadeEffect fadeEffect;

//    private void Awake()
//    {
//        if (instance == null)
//        {
//            instance = this;
//            DontDestroyOnLoad(gameObject);
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//    }

//    private void Start()
//    {
//        if (SceneManager.GetActiveScene().name != "TitleScene")
//        {
//            fadeEffect = FindObjectOfType<FadeEffect>();
//        }
//    }

//    public void FadeInOut(System.Action onFadeComplete)
//    {
//        if (fadeEffect == null)
//        {
//            fadeEffect = FindObjectOfType<FadeEffect>();
//            if (fadeEffect == null) return;
//        }

//        fadeEffect.StartFadeInOut(onFadeComplete);
//    }

//    public void LoadScene(string sceneName)
//    {
//        Debug.Log($"�� ��ȯ �õ�: {sceneName}");

//        //  TitleScene �� TrainingRoom �̵� �� ��� �� �ε� (���̵� ȿ�� X)
//        if (SceneManager.GetActiveScene().name == "TitleScene" && sceneName == "TrainingRoom")
//        {
//            Debug.Log(" TitleScene �� TrainingRoom ��� ��ȯ");
//            SceneManager.LoadScene(sceneName);
//            return;
//        }

//        //  �ٸ� �� ��ȯ �� ���̵� ȿ�� ���� (TrainingRoom �� Stage ��)
//        if (fadeEffect == null)
//        {
//            fadeEffect = FindObjectOfType<FadeEffect>();
//        }

//        fadeEffect?.StartFadeInOut(() =>
//        {
//            Debug.Log($" ���̵� ȿ�� ���� �� �� ��ȯ: {sceneName}");
//            SceneManager.LoadScene(sceneName);
//        });
//    }
//}





