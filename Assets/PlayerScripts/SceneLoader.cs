using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadTrainingRoom()
    {
        Debug.Log("버튼 클릭됨! LoadTrainingRoom 실행"); //  디버깅 추가
        GameManager.instance.LoadScene("TrainingRoom");
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
//        GameManager.instance.LoadScene("TraningRoom"); // TrainingRoom으로 Fade 적용 후 이동
//    }

//    public void ExitGame()
//    {
//        Debug.Log("게임 종료 시도");

//        // 실제 빌드된 게임에서는 종료
//        Application.Quit();

//        // Unity 에디터에서 실행 중이면 에디터 종료
//#if UNITY_EDITOR
//        UnityEditor.EditorApplication.isPlaying = false;
//#endif
//    }
//}



