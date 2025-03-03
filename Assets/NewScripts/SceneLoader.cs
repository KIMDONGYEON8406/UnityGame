using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

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

    public void LoadHouse()
    {
        StartCoroutine(LoadScene("House"));
    }

    public void LoadStage()
    {
        StartCoroutine(LoadScene("Stage"));
    }

    public void LoadTitle()
    {
        StartCoroutine(LoadScene("TitleScene"));
    }

    private IEnumerator LoadScene(string sceneName)
    {
        yield return SceneManager.LoadSceneAsync(sceneName);
        Debug.Log($"🔹 {sceneName} 씬으로 이동 완료");
    }

    public void ExitGame()
    {
        Debug.Log("게임 종료 시도");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}






















//public class SceneLoader : MonoBehaviour
//{
//    public void LoadTrainingRoom()
//    {
//        Debug.Log("버튼 클릭됨! LoadTrainingRoom 실행"); //  디버깅 추가
//        GameManager.instance.LoadScene("TrainingRoom");
//    }

//    public void ExitGame()
//    {
//        Debug.Log("게임 종료 시도");
//        Application.Quit();

//#if UNITY_EDITOR
//        UnityEditor.EditorApplication.isPlaying = false;
//#endif
//    }
//}






