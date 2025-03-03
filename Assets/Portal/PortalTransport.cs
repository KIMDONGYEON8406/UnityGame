using System.Collections;
using UnityEngine;

public class PortalTransport : MonoBehaviour
{
    public Transform targetPosition; // 🚀 포탈을 타면 이동할 목표 위치 (Inspector에서 설정)
    public GameObject portalUI; // 🚀 UI 오브젝트 (Inspector에서 연결)
    private bool isPlayerNear = false; // 🚀 플레이어가 포탈 근처에 있는지 확인

    private void Start()
    {
        if (portalUI != null)
        {
            portalUI.SetActive(false); // 🚀 시작할 때 UI 숨김
        }

        // 🚀 targetPosition이 설정되지 않았을 경우 자동으로 `SpawnPoint` 검색
        if (targetPosition == null)
        {
            GameObject spawnObj = GameObject.Find("SpawnPoint"); // 🚀 이름으로 찾기
            if (spawnObj != null)
            {
                targetPosition = spawnObj.transform;
                Debug.Log($"✅ PortalTransport: targetPosition이 자동으로 설정됨 → {targetPosition.position}");
            }
            else
            {
                Debug.LogError("❌ PortalTransport: SpawnPoint를 찾을 수 없음! Inspector에서 직접 설정 필요!");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("✅ 플레이어가 포탈 근처에 도착했습니다.");
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
            Debug.Log("🚪 플레이어가 포탈에서 멀어졌습니다.");
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
                Debug.Log("🟢 C 키 입력 감지됨! 포탈 이동 시작");
                portalUI.SetActive(false);

                // 🚀 FadeOut 실행 후 이동
                if (FadeManager.instance != null)
                {
                    Debug.Log("🎬 FadeOut 실행!");
                    FadeManager.instance.StartFadeOutThen(MovePlayer);
                }
                else
                {
                    Debug.LogError("❌ FadeManager.instance가 존재하지 않음!");
                    MovePlayer(); // 🚨 FadeOut이 안 되면 그냥 이동
                }
            }
            else if (Input.GetKeyDown(KeyCode.X)) // 🚀 X 키를 누르면 취소
            {
                portalUI.SetActive(false);
                Debug.Log("❌ 이동이 취소되었습니다.");
            }
        }
    }


    //private void Update()
    //{
    //    if (isPlayerNear && portalUI != null && portalUI.activeSelf) // 🚀 UI가 있을 때만 실행
    //    {
    //        if (Input.GetKeyDown(KeyCode.C)) // 🚀 C 키를 눌렀을 때 이동
    //        {
    //            Debug.Log("🟢 C 키 입력 감지됨! 포탈 이동 시작");
    //            portalUI.SetActive(false);

    //            // 🚨 FadeOut 없이 바로 이동 실행하여 원인 확인
    //            MovePlayer();
    //        }
    //        else if (Input.GetKeyDown(KeyCode.X)) // 🚀 X 키를 누르면 취소
    //        {
    //            portalUI.SetActive(false);
    //            Debug.Log("❌ 이동이 취소되었습니다.");
    //        }
    //    }
    //}

    private void MovePlayer()
    {
        Debug.Log("🟠 MovePlayer() 실행됨!");
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null && targetPosition != null)
        {
            Debug.Log($"🔹 {player.name}가 포탈을 통해 {targetPosition.position} 위치로 이동합니다.");

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

            Debug.Log($"🔹 {player.name}의 새로운 위치: {player.transform.position}");

            // 🚀 이동 후 FadeIn 실행
            FadeManager.instance?.StartFadeIn();
        }
        else
        {
            Debug.LogError("❌ PortalTransport: 이동할 플레이어나 targetPosition이 설정되지 않음!");
        }
    }
}






//public class PortalTransport : MonoBehaviour
//{
//    public Transform targetPosition; // 🚀 포탈을 타면 이동할 목표 위치 (Inspector에서 설정)
//    public GameObject portalUI; // 🚀 UI 오브젝트 (Inspector에서 연결)
//    private bool isPlayerNear = false; // 🚀 플레이어가 포탈 근처에 있는지 확인

//    private void Start()
//    {
//        if (portalUI != null)
//        {
//            portalUI.SetActive(false); // 🚀 시작할 때 UI 숨김
//        }

//        // 🚀 targetPosition이 설정되지 않았을 경우 자동으로 `SpawnPoint` 검색
//        if (targetPosition == null)
//        {
//            GameObject spawnObj = GameObject.Find("SpawnPoint"); // 🚀 태그 대신 이름으로 찾기
//            if (spawnObj != null)
//            {
//                targetPosition = spawnObj.transform;
//            }
//            else
//            {
//                Debug.LogError("❌ PortalTransport: SpawnPoint를 찾을 수 없음! Inspector에서 직접 설정 필요!");
//            }
//        }
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            isPlayerNear = true;
//            if (portalUI != null)
//            {
//                portalUI.SetActive(true); // 🚀 UI 활성화
//            }
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            isPlayerNear = false;
//            if (portalUI != null)
//            {
//                portalUI.SetActive(false); // 🚀 UI 숨김
//            }
//        }
//    }

//    private void Update()
//    {
//        if (isPlayerNear && portalUI != null && portalUI.activeSelf) // 🚀 UI가 있을 때만 실행
//        {
//            if (Input.GetKeyDown(KeyCode.C)) // 🚀 C 키를 눌렀을 때 이동
//            {
//                portalUI.SetActive(false);
//                FadeManager.instance?.StartFadeOutThen(MovePlayer); // 🚀 FadeOut 후 이동 실행
//            }
//            else if (Input.GetKeyDown(KeyCode.X)) // 🚀 X 키를 누르면 취소
//            {
//                portalUI.SetActive(false);
//                Debug.Log("❌ 이동이 취소되었습니다.");
//            }
//        }
//    }

//    private void MovePlayer()
//    {
//        GameObject player = GameObject.FindWithTag("Player");

//        if (player != null && targetPosition != null)
//        {
//            Debug.Log($"🔹 {player.name}가 포탈을 통해 {targetPosition.position} 위치로 이동합니다.");

//            CharacterController cc = player.GetComponent<CharacterController>();
//            if (cc != null)
//            {
//                cc.enabled = false; // 🚀 이동 전 CharacterController 비활성화
//            }

//            player.transform.position = targetPosition.position;
//            player.transform.rotation = targetPosition.rotation;

//            if (cc != null)
//            {
//                cc.enabled = true; // 🚀 이동 후 CharacterController 다시 활성화
//            }

//            Debug.Log($"🔹 {player.name}의 새로운 위치: {player.transform.position}");

//            // 🚀 이동 후 FadeIn 실행
//            FadeManager.instance?.StartFadeIn();
//        }
//        else
//        {
//            Debug.LogError("❌ PortalTransport: targetPosition이 설정되지 않음! Inspector에서 설정 필요!");
//        }
//    }
//}














//public class PortalTransport : MonoBehaviour
//{
//    public Transform defaultDestination; // 기본 목적지

//    public void MovePlayer(Collider player, Transform targetPosition)
//    {
//        if (targetPosition == null)
//        {
//            targetPosition = defaultDestination;
//        }

//        if (targetPosition == null)
//        {
//            Debug.LogError("PortalTransport: targetPosition이 설정되지 않음");
//            return;
//        }

//        Debug.Log($"플레이어 이동: {player.name} → {targetPosition.position}");

//        CharacterController cc = player.GetComponent<CharacterController>();

//        //  1. 플레이어 이동
//        if (cc != null)
//        {
//            cc.enabled = false; // 이동 전에 비활성화
//            player.transform.position = targetPosition.position + Vector3.up * 2.0f;
//            StartCoroutine(ReenableCharacterController(cc));
//        }
//        else
//        {
//            player.transform.position = targetPosition.position + Vector3.up * 2.0f;
//        }
//    }

//    private IEnumerator ReenableCharacterController(CharacterController cc)
//    {
//        yield return new WaitForSeconds(0.1f);

//        if (cc != null)
//        {
//            cc.enabled = true;
//            Debug.Log(" CharacterController 다시 활성화 완료");
//        }
//    }


//private IEnumerator ReenableCharacterController(CharacterController cc)
//{
//    yield return new WaitForSeconds(0.1f);
//    cc.enabled = true;
//}









//public class PortalTransport : MonoBehaviour
//{
//    public Transform defaultDestination; // 기본 목적지
//    public GameObject sword; // Sword 오브젝트

//    public void MovePlayer(Collider player, Transform targetPosition)
//    {
//        if (targetPosition == null)
//        {
//            targetPosition = defaultDestination;
//        }

//        if (targetPosition == null)
//        {
//            Debug.LogError("PortalTransport: targetPosition이 설정되지 않음");
//            return;
//        }

//        Debug.Log($"플레이어 이동 실행: {player.name} → {targetPosition.position}");

//        CharacterController cc = player.GetComponent<CharacterController>();
//        Rigidbody rb = player.GetComponent<Rigidbody>();

//        // 🔹 1. Sword 부모 해제 (루트 오브젝트로 이동)
//        if (sword != null)
//        {
//            sword.transform.SetParent(null);
//        }

//        // 🔹 2. 플레이어 이동 처리
//        if (cc != null)
//        {
//            Debug.Log("CharacterController 감지됨, 이동 처리 시작");
//            cc.enabled = false; // 이동 전에 비활성화
//            player.transform.position = targetPosition.position + Vector3.up * 2.0f;
//            StartCoroutine(ReenableCharacterController(cc));
//        }
//        else if (rb != null)
//        {
//            Debug.Log("Rigidbody 감지됨, Transform.position을 사용하여 이동");
//            player.transform.position = targetPosition.position + Vector3.up * 2.0f;
//        }
//        else
//        {
//            Debug.Log("CharacterController와 Rigidbody 없음, Transform.position으로 이동");
//            player.transform.position = targetPosition.position;
//        }

//        // 🔹 3. 일정 시간 후 Sword 복구
//        StartCoroutine(RestoreSwordSettings(player));
//    }

//    private IEnumerator ReenableCharacterController(CharacterController cc)
//    {
//        yield return new WaitForSeconds(0.1f); // 짧은 딜레이 후 활성화
//        cc.enabled = true;
//        Debug.Log("CharacterController 다시 활성화 완료, 이동 가능");
//    }

//    private IEnumerator RestoreSwordSettings(Collider player)
//    {
//        yield return new WaitForSeconds(0.2f);

//        if (sword != null)
//        {
//            //  weapon_r 위치 찾기
//            Transform handTransform = player.transform.Find("weapon_r");


//            //  찾은 위치를 로그로 출력
//            if (handTransform != null)
//            {
//                Debug.Log("🔹 weapon_r 찾음! Sword를 손에 다시 장착.");
//                sword.transform.SetParent(handTransform);
//                sword.transform.localPosition = Vector3.zero;
//                sword.transform.localRotation = Quaternion.identity;
//            }
//            else
//            {
//                Debug.LogError("❌ 손 위치를 찾을 수 없음! 검 복구 실패.");
//            }
//        }
//    }

//}





