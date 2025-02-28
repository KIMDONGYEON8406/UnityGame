using System.Collections;
using UnityEngine;

public class PortalTransport : MonoBehaviour
{
    public Transform defaultDestination; // 기본 목적지

    public void MovePlayer(Collider player, Transform targetPosition)
    {
        if (targetPosition == null)
        {
            targetPosition = defaultDestination;
        }

        if (targetPosition == null)
        {
            Debug.LogError("PortalTransport: targetPosition이 설정되지 않음");
            return;
        }

        Debug.Log($"플레이어 이동: {player.name} → {targetPosition.position}");

        CharacterController cc = player.GetComponent<CharacterController>();

        //  1. 플레이어 이동
        if (cc != null)
        {
            cc.enabled = false; // 이동 전에 비활성화
            player.transform.position = targetPosition.position + Vector3.up * 2.0f;
            StartCoroutine(ReenableCharacterController(cc));
        }
        else
        {
            player.transform.position = targetPosition.position + Vector3.up * 2.0f;
        }
    }

    private IEnumerator ReenableCharacterController(CharacterController cc)
    {
        yield return new WaitForSeconds(0.1f);

        if (cc != null)
        {
            cc.enabled = true;
            Debug.Log(" CharacterController 다시 활성화 완료");
        }
    }


    //private IEnumerator ReenableCharacterController(CharacterController cc)
    //{
    //    yield return new WaitForSeconds(0.1f);
    //    cc.enabled = true;
    //}
}








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





