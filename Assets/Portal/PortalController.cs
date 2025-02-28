using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PortalController : MonoBehaviour
{
    public GameObject portalUI;
    public Transform targetPosition;
    public PortalTransport portalTransport;
    private bool isPortalActive = false;
    private SkeletonSpawner spawner;

    private void Start()
    {
        spawner = FindObjectOfType<SkeletonSpawner>(); // 스포너 찾기

        UIManager.instance.SetUIVisibility(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OpenPortalUI();
        }
    }

    private void Update()
    {
        if (isPortalActive)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                GameManager.instance.FadeInOut(() =>
                {
                    PortalTransition();
                });
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                ClosePortalUI();
            }
        }
    }

    private void OpenPortalUI()
    {
        portalUI.SetActive(true);
        isPortalActive = true;
    }

    private void ClosePortalUI()
    {
        portalUI.SetActive(false);
        isPortalActive = false;
    }

    private void PortalTransition()
    {
        if (portalTransport != null)
        {
            portalTransport.MovePlayer(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>(), targetPosition);
        }

        ClosePortalUI();

        if (spawner != null)
        {
            spawner.ActivateEnemies(); // 포탈을 통과하면 적을 활성화
        }

        UIManager.instance.SetUIVisibility(true);
    }
}




//public class PortalController : MonoBehaviour
//{
//    public GameObject portalUI; // PortalUI 패널
//    public Transform targetPosition; // 이동할 목적지 위치 (Inspector에서 설정)
//    public PortalTransport portalTransport; // 포탈 이동 처리 스크립트 참조

//    private bool isPortalActive = false; // PortalUI 활성화 여부

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Player")) // 플레이어가 포탈에 닿았을 때만 UI 활성화
//        {
//            OpenPortalUI();
//        }
//    }

//    private void Update()
//    {
//        if (isPortalActive)
//        {
//            if (Input.GetKeyDown(KeyCode.C))
//            {
//                //  Fade 효과 적용 후 이동 실행
//                GameManager.instance.FadeInOut(() =>
//                {
//                    PortalTransition();
//                });
//            }
//            else if (Input.GetKeyDown(KeyCode.X))
//            {
//                ClosePortalUI();
//            }
//        }
//    }

//    private void OpenPortalUI()
//    {
//        portalUI.SetActive(true);
//        isPortalActive = true;
//    }

//    private void ClosePortalUI()
//    {
//        portalUI.SetActive(false);
//        isPortalActive = false;
//    }

//    private void PortalTransition()
//    {
//        //  UI 닫기 (Fade 후에도 남아있는 문제 해결)
//        ClosePortalUI();

//        //  플레이어 이동 실행
//        if (portalTransport != null)
//        {
//            portalTransport.MovePlayer(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>(), targetPosition);
//        }
//        else
//        {
//            Debug.LogError("PortalController: portalTransport가 설정되지 않았습니다.");
//        }
//    }
//}



