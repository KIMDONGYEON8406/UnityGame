using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"충돌 감지됨: {other.gameObject.name}"); //  충돌된 오브젝트 이름 출력

        if (other.CompareTag("Sword")) // 검과 충돌하면
        {
            Debug.Log(" 검과 충돌! 적 제거");
            Destroy(gameObject); // 적 삭제
        }
    }
}


