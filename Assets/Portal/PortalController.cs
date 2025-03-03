using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; //  씬 전환을 위해 추가


public class PortalController : MonoBehaviour
{
    public Transform targetPosition; // 🚀 이동할 목표 위치 (Inspector에서 설정)
    public GameObject portalUI; // 🚀 UI 오브젝트 (Inspector에서 연결)
    private bool isPlayerNear = false; // 🚀 플레이어가 포탈 근처에 있는지 확인

    private void Start()
    {
        if (portalUI == null)
        {
            portalUI = GameObject.Find("PortalUI"); // 🚀 자동으로 PortalUI 찾기
        }

        if (targetPosition == null)
        {
            GameObject spawnPointObj = GameObject.FindWithTag("SpawnPoint"); // 🚀 SpawnPoint 자동 감지
            if (spawnPointObj != null)
            {
                targetPosition = spawnPointObj.transform;
            }
        }

        if (portalUI != null)
        {
            portalUI.SetActive(false); // 🚀 시작할 때 UI 숨김
        }
        else
        {
            Debug.LogError("❌ PortalController: portalUI가 Inspector에서 설정되지 않음!");
        }

        if (targetPosition == null)
        {
            Debug.LogError("❌ PortalController: targetPosition이 설정되지 않음! Inspector에서 설정 필요!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            if (portalUI != null)
            {
                portalUI.SetActive(true); // 🚀 UI 활성화
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (portalUI != null)
            {
                portalUI.SetActive(false); // 🚀 UI 숨김
            }
        }
    }

    private void Update()
    {
        if (isPlayerNear && portalUI != null && portalUI.activeSelf) // 🚀 UI가 있을 때만 실행
        {
            if (Input.GetKeyDown(KeyCode.C)) // 🚀 C 키를 눌렀을 때 이동
            {
                FadeManager.instance?.StartFadeOutThen(MovePlayer);
            }
            else if (Input.GetKeyDown(KeyCode.X)) // 🚀 X 키를 누르면 취소
            {
                portalUI.SetActive(false);
                Debug.Log("❌ 이동이 취소되었습니다.");
            }
        }
    }

    private void MovePlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player == null)
        {
            Debug.LogError("❌ PortalController: Player를 찾을 수 없음!");
            return;
        }

        if (targetPosition == null)
        {
            Debug.LogError("❌ PortalController: targetPosition이 설정되지 않음!");
            return;
        }

        Debug.Log($"🔹 {player.name}가 포탈을 통해 이동합니다.");

        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null)
        {
            cc.enabled = false; // 🚀 이동 전 CharacterController 비활성화
        }

        player.transform.position = targetPosition.position;
        player.transform.rotation = targetPosition.rotation;

        if (cc != null)
        {
            cc.enabled = true; // 🚀 이동 후 CharacterController 다시 활성화
        }

        if (portalUI != null)
        {
            portalUI.SetActive(false); // 🚀 이동 후 UI 닫기
        }

        Debug.Log($"🔹 {player.name}의 새로운 위치: {player.transform.position}");
    }
}












//public class PortalController : MonoBehaviour
//{
//    public GameObject portalUI;
//    public Transform targetPosition;
//    public PortalTransport portalTransport;
//    private bool isPortalActive = false;
//    private SkeletonSpawner spawner;

//    private void Start()
//    {
//        spawner = FindObjectOfType<SkeletonSpawner>(); // 스포너 찾기

//        UIManager.instance.SetUIVisibility(false);
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Player"))
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
//        if (portalTransport != null)
//        {
//            portalTransport.MovePlayer(GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>(), targetPosition);
//        }

//        ClosePortalUI();

//        if (spawner != null)
//        {
//            spawner.ActivateEnemies(); // 포탈을 통과하면 적을 활성화
//        }

//        UIManager.instance.SetUIVisibility(true);
//    }
//}
