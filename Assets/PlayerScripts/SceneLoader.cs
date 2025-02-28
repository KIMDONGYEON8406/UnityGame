using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadTrainingRoom()
    {
        Debug.Log("��ư Ŭ����! LoadTrainingRoom ����"); //  ����� �߰�
        GameManager.instance.LoadScene("TrainingRoom");
    }

    public void ExitGame()
    {
        Debug.Log("���� ���� �õ�");
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
//        GameManager.instance.LoadScene("TraningRoom"); // TrainingRoom���� Fade ���� �� �̵�
//    }

//    public void ExitGame()
//    {
//        Debug.Log("���� ���� �õ�");

//        // ���� ����� ���ӿ����� ����
//        Application.Quit();

//        // Unity �����Ϳ��� ���� ���̸� ������ ����
//#if UNITY_EDITOR
//        UnityEditor.EditorApplication.isPlaying = false;
//#endif
//    }
//}



