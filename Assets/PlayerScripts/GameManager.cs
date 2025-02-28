using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private FadeEffect fadeEffect;

    public GameObject gameOverUI; // 게임 오버 UI 추가

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
        Debug.Log($"씬 전환 시도: {sceneName}");

        // TitleScene → TrainingRoom 이동 시 즉시 씬 로드 (페이드 효과 X)
        if (SceneManager.GetActiveScene().name == "TitleScene" && sceneName == "TrainingRoom")
        {
            Debug.Log("TitleScene → TrainingRoom 즉시 전환");
            SceneManager.LoadScene(sceneName);
            return;
        }

        // 다른 씬 전환 시 페이드 효과 적용 (TrainingRoom → Stage 등)
        if (fadeEffect == null)
        {
            fadeEffect = FindObjectOfType<FadeEffect>();
        }

        fadeEffect?.StartFadeInOut(() =>
        {
            Debug.Log($"페이드 효과 적용 후 씬 전환: {sceneName}");
            SceneManager.LoadScene(sceneName);
        });
    }

    //  게임 오버 처리 추가
    public void GameOver()
    {
        Debug.Log("게임 오버!");
        gameOverUI.SetActive(true); // 게임 오버 UI 활성화
        Time.timeScale = 0f; // 게임 일시 정지
    }

    //  게임 재시작 기능 추가
    public void RestartGame()
    {
        Time.timeScale = 1f; // 시간 정상화
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 현재 씬 다시 로드
    }

    //  메인 메뉴로 이동 기능 추가
    public void GoToTitle()
    {
        Time.timeScale = 1f; // 시간 정상화
        SceneManager.LoadScene("TitleScene"); // 메인 메뉴 씬으로 이동 (씬 이름 변경 가능)
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
//        Debug.Log($"씬 전환 시도: {sceneName}");

//        //  TitleScene → TrainingRoom 이동 시 즉시 씬 로드 (페이드 효과 X)
//        if (SceneManager.GetActiveScene().name == "TitleScene" && sceneName == "TrainingRoom")
//        {
//            Debug.Log(" TitleScene → TrainingRoom 즉시 전환");
//            SceneManager.LoadScene(sceneName);
//            return;
//        }

//        //  다른 씬 전환 시 페이드 효과 적용 (TrainingRoom → Stage 등)
//        if (fadeEffect == null)
//        {
//            fadeEffect = FindObjectOfType<FadeEffect>();
//        }

//        fadeEffect?.StartFadeInOut(() =>
//        {
//            Debug.Log($" 페이드 효과 적용 후 씬 전환: {sceneName}");
//            SceneManager.LoadScene(sceneName);
//        });
//    }
//}





